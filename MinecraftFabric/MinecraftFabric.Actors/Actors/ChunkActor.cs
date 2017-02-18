using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using System.Collections.Generic;
using Microsoft.ServiceFabric.Actors.Client;
using MinecraftFabric.Actors.Interfaces;

namespace MinecraftFabric.Actors
{
    class ChunkActor : Actor, IChunkActor
    {
        private IActorTimer _updateTimer;
        private Dictionary<ActorId, TrackingChunkMetaData> _tracking;

        public ChunkActor(ActorService actorService, ActorId actorId) : base(actorService, actorId)
        {
        }

        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "ChunkActor activated.");

            this.StateManager.TryAddStateAsync("initializeObservers", new Dictionary<ActorId, MinecraftVersion>());
            this.StateManager.TryAddStateAsync("observers", new Dictionary<ActorId, MinecraftVersion>());
            this.StateManager.TryAddStateAsync("associations", new Dictionary<ActorId, AssociateChunkMetaData>());
            this.StateManager.TryAddStateAsync("updateRateMS", 30);
            this.StateManager.TryAddStateAsync("worldActorID", new ActorId(""));
            this.StateManager.TryAddStateAsync("minLocation", new Position(0,0,0));
            this.StateManager.TryAddStateAsync("chunkStride", 0);

            _tracking = new Dictionary<ActorId, TrackingChunkMetaData>();

            return Task.FromResult(0);
        }
 
        public async Task<GenericResponse> Initialize(ActorId sessionID, Position minLocation, int chunkStride)
        {
            await this.StateManager.SetStateAsync("worldActorID", sessionID);
            await this.StateManager.SetStateAsync("minLocation", minLocation);
            await this.StateManager.SetStateAsync("chunkStride", chunkStride);
            return new GenericResponse();
        }

        public async Task<GenericResponse> Associate(ActorId informChunkActorId, ActorId blockPerChunkActorId, ActorId playersPerChunkActorId, int fidelity, Position position, int blockStride)
        {
            var associations = await this.StateManager.GetStateAsync<Dictionary<ActorId, AssociateChunkMetaData>>("associations");

            var metaData = new AssociateChunkMetaData(informChunkActorId, blockPerChunkActorId, playersPerChunkActorId, fidelity,position,blockStride);
            associations.Add(informChunkActorId, metaData);
            await this.StateManager.SetStateAsync<Dictionary<ActorId, AssociateChunkMetaData>>("associations", associations);
            return new GenericResponse();
        }

        public async Task<ActorId[]> GetAssociations()
        {
            var associations = await this.StateManager.GetStateAsync<Dictionary<ActorId, AssociateChunkMetaData>>("associations");
            if (associations.Count > 0)
            {
                var actorIDs = new ActorId[associations.Count];
                associations.Keys.CopyTo(actorIDs,0);
                return actorIDs;
            }

            return null;
        }

        public async Task<GenericResponse> SetResponseTime(int millisecond)
        {
            await this.StateManager.SetStateAsync<int>("updateRateMS", millisecond);
            return new GenericResponse();
        }

        public async Task<GenericResponse> RegisterObserver(ActorId playerAgentID, ActorId[] fromActors)
        {
            var players = await this.StateManager.GetStateAsync<Dictionary<ActorId,MinecraftVersion>>("observers");
            var version = MinecraftVersion.GetNext();
            
            //Is player already here?
            if (players.ContainsKey(playerAgentID) == true)
             {
                return new GenericResponse(true, "Already Exist");
            }

            //this player want to listen to all associated blocks
            var initPlayers = await this.StateManager.GetStateAsync<Dictionary<ActorId, MinecraftVersion>>("initializeObservers");
            initPlayers.Add(playerAgentID, version);
            await this.StateManager.SetStateAsync("initializeObservers", initPlayers);

            //map to hash for faster lookup
            var map = new HashSet<ActorId>();
            foreach (var actor in fromActors)
            {
                map.Add(actor);
            }

            var associations = await this.StateManager.GetStateAsync<Dictionary<ActorId, AssociateChunkMetaData>>("associations");
            //each associated grain needs to update this one
            foreach (var pair in associations)
            {
                var actorID = pair.Key;
                if (map.Contains(actorID) == false)
                {
                    var assocations = pair.Value;
                    assocations.needInitObservers.Add(actorID);
                }
            }

            await this.StateManager.SetStateAsync("associations", associations);

            //if no updates are occuring, now we have a player to update
            if (_updateTimer == null)
            {
                int updateRate = await this.StateManager.GetStateAsync<int>("updateRateMS");
                _updateTimer = RegisterTimer((_) => Fetch(), null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(updateRate));
            }

            return new GenericResponse();
        }

        public async Task<GenericResponse> UnRegisterObserver(ActorId playerAgentID)
        {
            var players = await this.StateManager.GetStateAsync<Dictionary<ActorId, MinecraftVersion>>("observers");

            if (players.ContainsKey(playerAgentID))
            {
                players.Remove(playerAgentID);
                await this.StateManager.SetStateAsync("observers", players);

                var initPlayers = await this.StateManager.GetStateAsync<Dictionary<ActorId, MinecraftVersion>>("initializeObservers");
                if (initPlayers.ContainsKey(playerAgentID))
                {
                    initPlayers.Remove(playerAgentID);
                    await this.StateManager.SetStateAsync("initializeObservers", initPlayers);
                }

                if (players.Count == 0)
                {
                    if (_updateTimer != null)
                    {
                        UnregisterTimer(_updateTimer);
                    }
                }

                return new GenericResponse();
            }

            return new GenericResponse(false, "Observer not found");
        }


        private async Task Fetch()
        {
 
            if (HasLastFetchFinished() == false)
            {
                return;
            }


            var associationsTask =  this.StateManager.GetStateAsync<Dictionary<ActorId, AssociateChunkMetaData>>("associations");
            var playersTask = this.StateManager.GetStateAsync<Dictionary<ActorId, MinecraftVersion>>("observers");
            var initPlayersTask = this.StateManager.GetStateAsync<Dictionary<ActorId, MinecraftVersion>>("initializeObservers");
            await Task.WhenAll(associationsTask, playersTask, initPlayersTask);

            var associations = associationsTask.Result;
            var players = playersTask.Result;
            var initPlayers = initPlayersTask.Result;
            
            CalculateDistribution(1000,4096);

            ActorId[] actorIds = null;

  
            if (players.Count != 0)
            {
                actorIds = new ActorId[players.Count];
                players.Keys.CopyTo(actorIds, 0);
            }

            foreach (var pair in associations)
            {
                var association = pair.Value;

                //if no tracking pieces, then recreate... this handle the re-hydrate of the actor
                if (_tracking.ContainsKey(pair.Key) == false)
                {
                    var trackingData = new TrackingChunkMetaData()
                    {
                        fidelity = association.fidelity
                    };

                    _tracking.Add(pair.Key, trackingData);
                }

                var tracking = _tracking[pair.Key];
 
                var task = tracking.updateTask;
                if (task != null)
                {
                    var response = task.Result;
                    tracking.playerVersion = response.lastPlayersVersion;
                    tracking.blockVersion = response.lastBlockVersion;
                }

                if ((association.needInitObservers.Count > 0) || (actorIds.Length > 0))
                {
                    var informActor = ActorProxy.Create<InformActor>(pair.Value.informActorId, this.ServiceUri);
                    tracking.updateTask = informActor.InformOfChange(actorIds, association, tracking);
                }

                association.needInitObservers.Clear();
                //need to save
            }

            //convert over...

            foreach(var pair in initPlayers)
            {
                players.Add(pair.Key, pair.Value);
            }

            initPlayers.Clear();

            //now save everyting
            var taskSetInit = this.StateManager.SetStateAsync("initializeObservers", initPlayers);
            var taskSetObserver =  this.StateManager.SetStateAsync("observers", players);
            var taskSetAssociation = this.StateManager.SetStateAsync("associations", associations);
            await Task.WhenAll(taskSetInit, taskSetObserver, taskSetAssociation);

            return;

        }

        private bool HasLastFetchFinished()
        {
            bool ready = true;
            //know how many you want to update...
            foreach (var pair in _tracking)
            {
                var tracking = pair.Value;

                //see if the last run is done for this request, and update 
                if (tracking.updateTask != null)
                {
                    var task = tracking.updateTask;
                    if (task.IsCompleted == false)
                    {
                        ready = false;
                    }
                }
            }

            return ready;
        }

        private void CalculateDistribution(int playerBandwidthCap, int blockBandwithCap)
        {
            //need to this chunk first as it is closet to player
            //need to recalculate what we expect out of each chunk
            //now tell the system to update the surrounding
            //know how many you want to update...
            int[] nBlocksLeft = { 0, 0, 0, 0 };
            int[] nPlayersLeft = { 0, 0, 0, 0 };
            float[] blocksPercents = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] playersPercents = { 1.0f, 1.0f, 1.0f, 1.0f };

            const int MaxFidelity = 3;
            foreach (var pair in _tracking)
            {
                var tracking = pair.Value;
                //see if the last run is done for this request, and update 
                if (tracking.updateTask != null)
                {
                    var result = tracking.updateTask.Result;
                    if (tracking.fidelity < MaxFidelity)
                    {
                        nBlocksLeft[tracking.fidelity] += result.availableBlocks;
                        nPlayersLeft[tracking.fidelity] += result.availablePlayers;
                    }
                    else
                    {
                        nBlocksLeft[MaxFidelity] += result.availableBlocks;
                        nPlayersLeft[MaxFidelity] += result.availablePlayers;
                    }
                }
            }


            int availablePlayers = playerBandwidthCap;
            int availableBlocks = blockBandwithCap;
            for (int idx = 0; idx <= MaxFidelity; idx++)
            {
                availablePlayers = FigureCountPerFidelity(nPlayersLeft, playersPercents, idx, MaxFidelity, availablePlayers);
                availableBlocks = FigureCountPerFidelity(nBlocksLeft, blocksPercents, idx, MaxFidelity, availableBlocks);
            }

            //fidelity 0 is always 100%
            playersPercents[0] = 1.0f;
            blocksPercents[0] = 1.0f;

            foreach (var pair in _tracking)
            {
                var tracking = pair.Value;
                int index = tracking.fidelity;
                if (index > MaxFidelity)
                {
                    index = MaxFidelity;
                }

                if (tracking.updateTask != null)
                {
                    var result = tracking.updateTask.Result;

                    tracking.maxPlayers = (int)(playersPercents[index] * result.availablePlayers);
                    tracking.maxBlocks = (int)(blocksPercents[index] * result.availableBlocks);
                }
                else
                {
                    tracking.maxPlayers = (int)(playersPercents[index] * playerBandwidthCap);
                    tracking.maxBlocks = (int)(blocksPercents[index] * blockBandwithCap);
                }

            }
        }

        private int FigureCountPerFidelity(int[] whatLefts, float[] whatPercent, int fidelity, int MaxFidelity, int availablePlayers)
        {
            if ((availablePlayers > 0) && (whatLefts[fidelity] > 0))
            {
                if (whatLefts[fidelity] > availablePlayers)
                {
                    for (int idx = fidelity + 1; idx <= MaxFidelity; idx++)
                    {
                        whatPercent[idx] = 0;
                    }

                    availablePlayers = 0;
                    whatPercent[fidelity] = whatLefts[fidelity] / availablePlayers;
                }
                else
                {
                    whatPercent[fidelity] = 1.0f;
                    availablePlayers -= whatLefts[fidelity];
                }
            }

            return availablePlayers;
        }
    }
}
 

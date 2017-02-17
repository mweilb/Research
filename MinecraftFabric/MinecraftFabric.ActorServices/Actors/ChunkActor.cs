using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using MinecraftFabric.ActorServices.DataContracts;
using MinecraftFabric.ActorServices.Interfaces;
using MinecraftFabric.ActorServices.TaskResponses;
using System.Collections.Generic;
using Microsoft.ServiceFabric.Actors.Client;

namespace MinecraftFabric.ActorServices.Actors
{
    class ChunkActor : Actor, IChunkActor
    {
        private IActorTimer _updateTimer;

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
            return Task.FromResult(0);
        }
 
        public async Task<GenericResponse> Initialize(ActorId sessionID, Position minLocation, int chunkStride)
        {
            await this.StateManager.SetStateAsync("worldActorID", sessionID);
            await this.StateManager.SetStateAsync("minLocation", minLocation);
            await this.StateManager.SetStateAsync("chunkStride", chunkStride);
            return new GenericResponse();
        }

        public async Task<GenericResponse> Associate(ActorId sessionID, ActorId actorID, int fidelity, Position position, int blockStride)
        {
            var associations = await this.StateManager.GetStateAsync<Dictionary<ActorId, AssociateChunkMetaData>>("associations");

            var metaData = new AssociateChunkMetaData(actorID,fidelity,position,blockStride);
            associations.Add(actorID, metaData);
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


            var associations = await this.StateManager.GetStateAsync<Dictionary<ActorId, AssociateChunkMetaData>>("associations");

            //map to hash for faster lookup
            var map = new HashSet<ActorId>();
            foreach (var actor in fromActors)
            {
                map.Add(actor);
            }

            //each associated grain needs to update this one
            foreach (var pair in associations)
            {
                var actorID = pair.Key;
                if (map.Contains(actorID) == false)
                {
                    var assoicationMetaData = pair.Value;
                    assoicationMetaData.needInitObservers.Add(actorID);
                }
            }


            //if no updates are occuring, now we have a player to update
            if (_updateTimer == null)
            {
                int updateRate = await this.StateManager.GetStateAsync<int>("initializeObservers");
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

 
        public Task<InformOfChunkChangeResponse> InformOfChange(IChunkActor actor, MinecraftVersion lastPlayerVersion, int suggestedMaxPlayerRequests, MinecraftVersion lastBlockVersion, int suggestedMaxBlockRequests)
        {
            throw new NotImplementedException();
        }

        private async Task Fetch()
        {
            var associations = await this.StateManager.GetStateAsync<Dictionary<ActorId, AssociateChunkMetaData>>("associations");

            if (HasLastFetchFinished(associations) == false)
            {
                return;
            }

            CalculateDistribution(associations,1000,4096);

            foreach (var pair in associations)
            {
                var tracking = pair.Value;
 
                var task = tracking.updateTask;
                if (task != null)
                {
                    var response = task.Result;
                    tracking.playerVersion = response.lastPlayersVersion;
                    tracking.blockVersion = response.lastBlockVersion;
                }

                if ((tracking.needInitObservers.Count > 0) || (observers.Count > 0))
                {
                    var actorID = tracking.actorID;
                    var chunkActor = ActorProxy.Create<ChunkActor>(actorID, this.ServiceUri);

                    tracking.updateTask = chunkActor.InformOfChange(tracking.needInitObservers, observers, tracking.playerVersion, tracking.maxPlayers, tracking.blockVersion, tracking.maxBlocks);
                }

                tracking.needInitObservers.Clear();
                //need to save
            }

            //covert over...
            mObservers.AddRange(mNeedsInitObservers);
            mNeedsInitObservers.Clear();

            return;

        }

        private bool HasLastFetchFinished(Dictionary<ActorId, AssociateChunkMetaData> associations)
        {
            bool ready = true;
            //know how many you want to update...
            foreach (var pair in associations)
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

        private void CalculateDistribution(Dictionary<ActorId, AssociateChunkMetaData> associations, int playerBandwidthCap, int blockBandwithCap)
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
            foreach (var pair in associations)
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

            foreach (var pair in associations)
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
 

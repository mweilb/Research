using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using MinecraftFabric.ActorServices.DataContracts;
using MinecraftFabric.ActorServices.Interfaces;
using MinecraftFabric.ActorServices.TaskResponses;
using System.Collections.Generic;

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
            players.Add(playerAgentID, version);
            await this.StateManager.SetStateAsync("observers", players);


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
                _updateTimer = RegisterTimer((_) => Fetch(), null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(mUpdateRateinMilliSeconds));
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

        private Task Fetch()
        {
        
            return Task.FromResult(true);
        }
    }
}
 

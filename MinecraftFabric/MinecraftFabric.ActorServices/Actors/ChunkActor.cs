using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using MinecraftFabric.ActorServices.DataContracts;
using MinecraftFabric.ActorServices.Interfaces;
using MinecraftFabric.ActorServices.TaskResponses;

namespace MinecraftFabric.ActorServices.Actors
{
    class ChunkActor : Actor, IChunkActor
    {
        public ChunkActor(ActorService actorService, ActorId actorId) : base(actorService, actorId)
        {
        }

        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "PlayerAgent activated.");
            return Task.FromResult(0);
        }

        public Task<GenericResponse> Associate(ActorId sessionID, ActorId chunkID, int fidelity, Position position, int blockStride)
        {
            throw new NotImplementedException();
        }

        public Task<InformOfChunkChangeResponse> InformOfChange(IChunkActor actor, MinecraftVersion lastPlayerVersion, int suggestedMaxPlayerRequests, MinecraftVersion lastBlockVersion, int suggestedMaxBlockRequests)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse> Initialize(ActorId sessionID, Position minLocation, int chunkStride)
        {
            throw new NotImplementedException();
        }

        public Task<BlockMetaData[]> InspectBlocks()
        {
            throw new NotImplementedException();
        }

        public Task<PlayerMetaData[]> InspectPlayers()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse> LeavePlayer(ActorId playerAgentID)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse> SetResponseTime(int millisecond)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse> StartPlayer(ActorId playerAgentID, Vector pos, string[] fromActors)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse> UpdateBlock(ActorId playerAgentID, BlockMetaData blockUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdatePlayer(long playerAgentID, Vector pos)
        {
            throw new NotImplementedException();
        }
    }
}
 

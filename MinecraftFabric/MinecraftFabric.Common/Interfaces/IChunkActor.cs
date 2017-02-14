using System.Collections.Generic;
using System.Threading.Tasks;
using MinecraftFabric.ActorServices.TaskResponses;
using MinecraftFabric.ActorServices.DataContracts;
using Microsoft.ServiceFabric.Actors;
using System;

namespace MinecraftFabric.ActorServices.Interfaces
{
    public interface IChunkActor : IActor
    {
        Task<GenericResponse> Initialize(ActorId sessionID, Position minLocation, Position maxLocation);
        Task<GenericResponse> Associate(ActorId sessionID, ActorId chunkID, int fidelity, Position position, int blockStride);
        Task<GenericResponse> SetResponseTime(int millisecond);

        Task<GenericResponse> StartPlayer(ActorId playerAgentID, Vector pos, string[] fromActors);
        Task<string> UpdatePlayer(Int64 playerAgentID, Vector pos);
        Task<GenericResponse> LeavePlayer(ActorId playerAgentID);

        Task<GenericResponse> UpdateBlock(ActorId playerAgentID, BlockMetaData blockUpdate);

        Task<InformOfChunkChangeResponse> InformOfChange(IChunkActor actor, MinecraftVersion lastPlayerVersion, int suggestedMaxPlayerRequests, MinecraftVersion lastBlockVersion, int suggestedMaxBlockRequests);

        //Not for simulation, but for testing
        Task<PlayerMetaData[]> InspectPlayers();

        Task<BlockMetaData[]> InspectBlocks();
    }
}

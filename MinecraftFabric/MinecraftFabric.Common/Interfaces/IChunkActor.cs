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
        Task<GenericResponse> Initialize(ActorId sessionID, Position minLocation, int chunkStride);
        Task<GenericResponse> Associate(ActorId sessionID, ActorId chunkID, int fidelity, Position position, int blockStride);
        Task<GenericResponse> SetResponseTime(int millisecond);

        Task<GenericResponse> RegisterObserver(ActorId playerAgentID, string[] fromActors);
        
        Task<GenericResponse> UnRegisterObserver(ActorId playerAgentID);

        Task<InformOfChunkChangeResponse> InformOfChange(IChunkActor actor, MinecraftVersion lastPlayerVersion, int suggestedMaxPlayerRequests, MinecraftVersion lastBlockVersion, int suggestedMaxBlockRequests);

    }
}

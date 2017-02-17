using Microsoft.ServiceFabric.Actors;
using MinecraftFabric.ActorServices.DataContracts;
using MinecraftFabric.ActorServices.TaskResponses;
using System;
using System.Threading.Tasks;

namespace MinecraftFabric.ActorServices.Interfaces
{
    public interface IBlocksPerChunkActor : IActor
    {
        Task<GenericResponse> Update(Int64 playerAgentID, BlockMetaData blockData);
        Task<GenericResponse> Remove(ActorId playerAgentID, BlockMetaData blockData);
        Task<BlockMetaData[]> GetAsync();
    }
}

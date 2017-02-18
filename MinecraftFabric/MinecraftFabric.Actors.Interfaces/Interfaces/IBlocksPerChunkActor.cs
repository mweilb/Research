using Microsoft.ServiceFabric.Actors;
using System;
using System.Threading.Tasks;

namespace MinecraftFabric.Actors.Interfaces
{
    public interface IBlocksPerChunkActor : IActor
    {
        Task<GenericResponse> Update(Int64 playerAgentID, BlockMetaData blockData);
        Task<GenericResponse> Remove(ActorId playerAgentID, BlockMetaData blockData);
        Task<BlockMetaData[]> GetAsync();
    }
}

 
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
 

namespace MinecraftFabric.Actors.Interfaces
{
    /// <summary>
    /// WorldActor tracks the associated chunks in the world
    /// </summary>
    public interface IWorldActor : IActor
    {
        /// <summary>
        /// Created the world based on request dimensions of the world
        /// </summary>
        /// <returns>GenericResponse</returns>
        Task<GenericResponse> Create(Position beginPoint, Position endPoint, int chunkSize, int visibilityChunkCount);

        /// <summary>
        /// Retrieve information about the world
        /// </summary>
        /// <returns>GetWorldInfoResponse</returns>
        Task<WorldInfoMetaData> GetInfo();
    }
}

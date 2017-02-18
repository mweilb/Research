using Microsoft.ServiceFabric.Actors;
using System.Threading.Tasks;

namespace MinecraftFabric.Actors.Interfaces
{
    public interface IPlayersPerChunkActor : IActor
    {
        Task<GenericResponse> Update(ActorId playerAgentID, PlayerMetaData playerMetaData);
        Task<GenericResponse> Remove(ActorId playerAgentID);
        Task<PlayerMetaData[]> GetAsync();
    }
}

using Microsoft.ServiceFabric.Actors;
using System.Threading.Tasks;

namespace MinecraftFabric.Actors.Interfaces
{
    public interface IPlayerAgent : IActor, IActorEventPublisher<IChunkUpdateEvent> 
    {
        Task Update(BlockMetaData[] blocks, PlayerMetaData[] players);
        Task Init(BlockMetaData[] blocks, PlayerMetaData[] players);

    }
}

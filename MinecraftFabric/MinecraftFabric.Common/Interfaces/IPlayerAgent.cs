using Microsoft.ServiceFabric.Actors;
using MinecraftFabric.ActorServices.DataContracts;
using MinecraftFabric.ActorServices.Events;
using System.Threading.Tasks;

namespace MinecraftFabric.ActorServices.Interfaces
{
    public interface IPlayerAgent : IActor, IActorEventPublisher<IChunkUpdateEvent> 
    {
        Task Update(BlockMetaData[] blocks, PlayerMetaData[] players);
        Task Init(BlockMetaData[] blocks, PlayerMetaData[] players);

    }
}

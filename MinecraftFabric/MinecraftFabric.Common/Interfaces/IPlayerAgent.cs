using Microsoft.ServiceFabric.Actors;
using MinecraftFabric.ActorServices.DataContracts;
using MinecraftFabric.ActorServices.Events;

namespace MinecraftFabric.ActorServices.Interfaces
{
    public interface IPlayerAgent : IActor, IActorEventPublisher<IChunkUpdateEvent> 
    {
        void Update(BlockMetaData[] blocks, PlayerMetaData[] players);
        void Init(BlockMetaData[] blocks, PlayerMetaData[] players);
    }
}

using Microsoft.ServiceFabric.Actors;
using MinecraftFabric.ActorServices.DataContracts;

namespace MinecraftFabric.ActorServices.Events
{
    public interface IChunkUpdateEvent : IActorEvents
    {
        void Update(BlockMetaData[] blocks, PlayerMetaData[] players);
        void Init(BlockMetaData[] blocks, PlayerMetaData[] players);
    }

   
}

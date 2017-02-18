using Microsoft.ServiceFabric.Actors;

namespace MinecraftFabric.Actors.Interfaces
{
    public interface IChunkUpdateEvent : IActorEvents
    {
        void Update(BlockMetaData[] blocks, PlayerMetaData[] players);
        void Init(BlockMetaData[] blocks, PlayerMetaData[] players);
    }

   
}

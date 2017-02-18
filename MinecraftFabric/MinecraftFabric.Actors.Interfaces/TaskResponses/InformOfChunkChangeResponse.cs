
using Microsoft.ServiceFabric.Actors;

namespace MinecraftFabric.Actors.Interfaces
{
    public class InformOfChunkChangeResponse
    {
        public ActorId actorID;
        public MinecraftVersion lastBlockVersion;
        public int availableBlocks;
        public MinecraftVersion lastPlayersVersion;
        public int availablePlayers;
    }
}

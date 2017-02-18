
using Microsoft.ServiceFabric.Actors;
using MinecraftFabric.ActorServices.DataContracts;

namespace MinecraftFabric.ActorServices.TaskResponses
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

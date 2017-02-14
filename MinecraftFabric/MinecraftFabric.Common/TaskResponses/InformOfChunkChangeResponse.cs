
using MinecraftFabric.ActorServices.DataContracts;

namespace MinecraftFabric.ActorServices.TaskResponses
{
    public class InformOfChunkChangeResponse
    {
        public string mChunkID;
        public MinecraftVersion mLastBlockVersion;
        public int mAvailableBlocks;
        public MinecraftVersion mLastPlayersVersion;
        public int mAvailablePlayers;
    }
}

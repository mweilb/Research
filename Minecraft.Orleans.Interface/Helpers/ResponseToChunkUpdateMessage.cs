using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.OrleansInterfaces
{
    public class ResponseToChunkUpdateMessage
    {
        public string mChunkID;
        public MinecraftVersion mLastBlockVersion;
        public int mAvailableBlocks;
        public MinecraftVersion mLastPlayersVersion;
        public int mAvailablePlayers;
    }
}

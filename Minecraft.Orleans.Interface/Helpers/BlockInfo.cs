using System;
using Orleans.Concurrency;

namespace Minecraft.OrleansInterfaces
{

    [Immutable]
    public class BlockInfo 
    {
        public IntVec3 mPosition { get; set; }
        public string mID { get; set; }
        public Int32 mData { get; set; }
        public Int32 mDataExtended { get; set; }
        public MinecraftVersion mVersion { get; set; }

        public string mCreatorID { get; set; }
        public string mLastTouchedID { get; set; }

    }
}

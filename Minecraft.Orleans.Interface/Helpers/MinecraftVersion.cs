using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.OrleansInterfaces
{
    public class MinecraftVersion
    {
        private DateTime mID;

        public static MinecraftVersion MinValue = new MinecraftVersion();

        public MinecraftVersion()
        {
            mID = DateTime.MinValue;
        }
        public static MinecraftVersion GetNext()
        {
            MinecraftVersion version = new MinecraftVersion();
            version.mID = DateTime.Now;
            return version;
        }

        public MinecraftVersion GetNextTick()
        {
            MinecraftVersion version = new MinecraftVersion();
            version.mID = this.mID;
            version.mID.AddTicks(1);
            return version;
        }
        public static bool operator >(MinecraftVersion x, MinecraftVersion y)
        {
            return (x.mID > y.mID);
        }

        public static bool operator <(MinecraftVersion x, MinecraftVersion y)
        {
            return (x.mID < y.mID);
        }

        public static bool operator >=(MinecraftVersion x, MinecraftVersion y)
        {
            return (x.mID >= y.mID);
        }

        public static bool operator <=(MinecraftVersion x, MinecraftVersion y)
        {
            return (x.mID <= y.mID);
        }
    }
}

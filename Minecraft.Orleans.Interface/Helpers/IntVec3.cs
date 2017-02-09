using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.OrleansInterfaces
{
    public class IntVec3
    {

        public IntVec3(int _x, int _y, int _z)
        {
            Set(_x, _y, _z);
        }

        public IntVec3(IntVec3 newVec3)
        {
            Set(newVec3.x, newVec3.y, newVec3.z);
        }

        public IntVec3()
        {
            Set(0, 0, 0);
        }

        public void Set(int _x, int _y, int _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public Guid ToGuid()
        {
            short y1 = (short)(y & 0xFFFF);
            short y2 = (short)(((short)(y >> 16)) & 0xFFFF);
            byte z1 = (byte)((z >> 0) & 0xFF);
            byte z2 = (byte)((z >> 8) & 0xFF);
            byte z3 = (byte)((z >> 16) & 0xFF);
            byte z4 = (byte)((z >> 24) & 0xFF);

            return new Guid(x, y1, y2, z1, z2, z3, z4, 0, 0, 0, 0);
        }

        public override string ToString()   
        {
            return string.Concat(x, ":", y, ":", z);
        }
        public int x, y, z;
    }
}


namespace Minecraft.OrleansInterfaces
{
    public class Vec3
    {
        public Vec3(float _x, float _y, float _z)
        {
            Set(_x, _y, _z);
        }

        public void Set(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public void Set(Vec3 newVec)
        {
            x = newVec.x;
            y = newVec.y;
            z = newVec.z;
        }

        public float x, y, z;
    }
}

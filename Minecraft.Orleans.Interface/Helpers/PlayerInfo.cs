 
namespace Minecraft.OrleansInterfaces
{
    public class PlayerInfo
    {
        public Vec3 mPosition { get; set; }
        public string mID { get; set; }
 
        public MinecraftVersion mVersion { get; set; }
        public string mCreatorID { get; set; }
        public string mLastTouchedID { get; set; }
    }
}

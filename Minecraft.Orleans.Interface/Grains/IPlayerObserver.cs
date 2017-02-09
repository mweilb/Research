using Minecraft.OrleansInterfaces;
using Orleans;

namespace Minecraft.OrleansInterfaces.Grains
{
    public interface IPlayerObserver :  IGrainObserver
    {
        void Update(BlockInfo[] blocks, int nBlocks, PlayerInfo[] players, int nPlayers);
    }
}

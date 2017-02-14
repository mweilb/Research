using Minecraft.OrleansInterfaces;
using Minecraft.OrleansInterfaces.Grains;
using System.Threading.Tasks;
using Orleans;

namespace Minecraft.OrleansClasses.Grains
{
    public partial class ChunkGrain : Grain, IChunkGrain
    {
        public Task<PlayerInfo[]> InspectPlayers()
        {
            PlayerInfo[] playerInfo = null;
            if (mActivePlayers.Count > 0)
            {
                playerInfo = new PlayerInfo[mActivePlayers.Count];
                mActivePlayers.CopyTo(playerInfo, 0);
            }
            return Task.FromResult<PlayerInfo[]>(playerInfo);
        }

        public Task<BlockInfo[]> InspectBlocks()
        {
            BlockInfo[] blockInfo = null;
            if (mBlocks.Count > 0)
            {
                blockInfo = new BlockInfo[mBlocks.Count];
                mBlocks.CopyTo(blockInfo, 0);
            }
            return Task.FromResult<BlockInfo[]>(blockInfo);
        }
    }
}

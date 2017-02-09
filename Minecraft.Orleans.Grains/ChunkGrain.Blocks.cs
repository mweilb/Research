using Minecraft.OrleansInterfaces;
using Minecraft.OrleansInterfaces.Grains;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;

namespace Minecraft.OrleansClasses.Grains
{
    public partial class ChunkGrain : Grain, IChunkGrain
    {
        List<BlockInfo> mBlocks;

        public Task<FeedbackMessage> UpdateBlock(string playerSessionID, BlockInfo blockUpdate)
        {
            //remove previous version
            int idx = mBlocks.FindIndex(item => ((item.mPosition.x == blockUpdate.mPosition.x) && (item.mPosition.y == blockUpdate.mPosition.y) && (item.mPosition.z == blockUpdate.mPosition.z)));
            if (idx >= 0)
            {
                mBlocks.RemoveAt(idx);
            }

            //add new version at end
            MinecraftVersion version = MinecraftVersion.GetNext();
            blockUpdate.mVersion = version;
            mBlocks.Add(blockUpdate);

            return Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.None));
        }
    }
}

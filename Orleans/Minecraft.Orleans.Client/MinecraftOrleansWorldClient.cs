
using Orleans;
using Minecraft.OrleansInterfaces.Grains;
using Minecraft.OrleansInterfaces;
using System.Threading.Tasks;

namespace Minecraft.Orleans.Client
{
    public class  MinecraftOrleansWorldClient 
    {
        string mSessionID;
        IWorldGrain mWorldGrain = null;
        IntVec3 mChunkSize = null;
        IntVec3 mMinLocation;
        IntVec3 mMaxLocation;

        public async Task<FeedbackMessage> Initialize(string sessionID)
        {
            mSessionID = sessionID;
            mWorldGrain = GrainClient.GrainFactory.GetGrain<IWorldGrain>(sessionID);

            var info = await mWorldGrain.GetInfo();
            mChunkSize = info.chunkSize;
            mMinLocation = info.minLocation;
            mMaxLocation = info.maxLocation;
   
            return new FeedbackMessage();

        }
        public Task<int> GetNumberOffChunks()
        {
            return mWorldGrain.GetNumberOffChunks();
        }

 
        public string GetChunk(Vec3 pos)
        {
            string grainId = "";
            if (mChunkSize != null && mSessionID != "")
            {
                int x = ((int)pos.x / mChunkSize.x) * mChunkSize.x;
                int y = ((int)pos.y / mChunkSize.y) * mChunkSize.y;
                int z = ((int)pos.z / mChunkSize.z) * mChunkSize.z;

                if ((mMinLocation.x <= x) && (x <= mMaxLocation.x) &&
                    (mMinLocation.y <= y) && (y <= mMaxLocation.y) &&
                    (mMinLocation.z <= z) && (z <= mMaxLocation.z))
                {
                    grainId = string.Concat(mSessionID, "#", x, "#", y, "#", z);
                }
            }

            return grainId;
        }

        public string GetChunk(IntVec3 pos)
        {
            Vec3 vec3 = new Vec3(pos.x, pos.y, pos.z);
            return GetChunk(vec3);
        }

        public Task<FeedbackMessage> UpdateBlock(string playerID, BlockInfo info)
        {
            if (info == null)
            {
                return Task.FromResult(new FeedbackMessage(FeedbackMessage.Responces.Error, "Invalidate info parameter"));
            }
            string id =   GetChunk(info.mPosition);
            if (id == "")
            {
                return Task.FromResult(new FeedbackMessage(FeedbackMessage.Responces.Error, "Block position outside the world"));
            }

            var chunkGrain = GrainClient.GrainFactory.GetGrain<IChunkGrain>(id);
            return chunkGrain.UpdateBlock(playerID, info);
        }

        //Not for simulation, but for testing
        public Task<PlayerInfo[]> InspectPlayers(IntVec3 pos)
        {
            string id = GetChunk(pos);
            var chunkGrain = GrainClient.GrainFactory.GetGrain<IChunkGrain>(id);
            var playerInfo = chunkGrain.InspectPlayers();
            return playerInfo;
        }

        public  Task<BlockInfo[]> InspectBlocks(IntVec3 pos)
        {
            string id = GetChunk(pos);
            var chunkGrain = GrainClient.GrainFactory.GetGrain<IChunkGrain>(id);
            var blockInfo =   chunkGrain.InspectBlocks();
            return blockInfo;

        }

    }
}

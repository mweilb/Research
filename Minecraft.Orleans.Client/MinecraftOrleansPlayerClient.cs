using Minecraft.OrleansInterfaces;
using Minecraft.OrleansInterfaces.Grains;
using Orleans;
using System.Threading.Tasks;

namespace Minecraft.Orleans.Client
{
    public class MinecraftOrleansPlayerClient 
    {
        private MinecraftOrleansWorldClient mWorld;
        private IChunkGrain mCurGrain=null;
        private IPlayerObserver mObserver = null;
        private string mPlayerID;

        public async Task<FeedbackMessage> Initialize(MinecraftOrleansWorldClient world, string playerID, IPlayerObserver observer, Vec3 spawnPoint)
        {
            mWorld = world;
            mPlayerID = playerID;
        
            mObserver = await GrainClient.GrainFactory.CreateObjectReference<IPlayerObserver>(observer);

            var grainID = mWorld.GetChunk(spawnPoint);
            if (grainID != "")
            {
                mCurGrain = GrainClient.GrainFactory.GetGrain<IChunkGrain>(grainID);     
                return await mCurGrain.StartPlayer(mPlayerID, mObserver, spawnPoint, null);
                
            }

            return await Task.FromResult(new FeedbackMessage(FeedbackMessage.Responces.Error, "Spawn point not in available Chunk"));
        }

        public async Task<FeedbackMessage> Update(Vec3 position)
        {
            if (mCurGrain != null)
            {
                var grainID = await mCurGrain.UpdatePlayer(mPlayerID, mObserver, position);
                if (grainID != null)
                {
                    mCurGrain = GrainClient.GrainFactory.GetGrain<IChunkGrain>(grainID);
                }
                else
                {
                    return await Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.Error, "Update - player not found"));
                }
                return await Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.None));
            }

            return await Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.Error, "Update was not initialized first or null chunk returned"));

        }

        public async Task<FeedbackMessage> Leave()
        {
            if (mCurGrain != null)
            {
                return await mCurGrain.LeavePlayer(mPlayerID);
            }
            return await Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.Error, "Leave was not initialized first or null chunk returned"));
        }

        public void TESTONLYCALL_CHANGEPLAYERNAME(string name)
        {
            mPlayerID = name;
        }

        public void TESTONLYCALL_SETRESPONSETIME(int milliseconds)
        {
            if (mCurGrain != null)
            {
                mCurGrain.SetResponseTime(milliseconds);
            }
        }
        

    }
}

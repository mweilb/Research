using Minecraft.OrleansInterfaces;
using Minecraft.OrleansInterfaces.Grains;
using Orleans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minecraft.OrleansClasses.Grains
{
    public partial class ChunkGrain : Grain, IChunkGrain
    {
        class ChunkTracking
        {
            public IChunkGrain mGrain;
            public int mFidelity;
            public int mMaxPlayers;
            public MinecraftVersion mPlayerVersion;
            public int mMaxBlocks;
            public MinecraftVersion mBlockVersion;
            public IntVec3 mMaxLocation;
            public IntVec3 mMinLocation;
            public List<IPlayerObserver> mNeedInitObservers;
            public Task<ResponseToChunkUpdateMessage> mUpdateTask = null;

            public ChunkTracking()
            {
                mNeedInitObservers = new List<IPlayerObserver>();
            }
        }

        Dictionary<string, ChunkTracking> mAssociatedGrains;

        public override Task OnActivateAsync()
        {
            mNeedsInitObservers = new List<IPlayerObserver>();
            mObservers = new List<IPlayerObserver>();
            mBlocks = new List<BlockInfo>();
            mAssociatedGrains = new Dictionary<string, ChunkTracking>();
            mTimer = null;
            mActivePlayers = new List<PlayerInfo>();

            return TaskDone.Done;
        }

        public Task<FeedbackMessage> Initialize(string id, IntVec3 minLocation, IntVec3 maxLocation)
        {
            mID = id;
            mMinLocation = minLocation;
            mMaxLocation = maxLocation;
            return Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.None));
        }

        public Task<FeedbackMessage> Associate(string id, string grainID, int fidelity, IntVec3 minLocation, IntVec3 maxLocation)
        {
            IChunkGrain chunkGrain = GrainFactory.GetGrain<IChunkGrain>(grainID);
            var chunkTracking = new ChunkTracking();
            chunkTracking.mGrain = chunkGrain;
            chunkTracking.mFidelity = fidelity;
            chunkTracking.mMinLocation = minLocation;
            chunkTracking.mPlayerVersion = MinecraftVersion.MinValue;
            chunkTracking.mBlockVersion = MinecraftVersion.MinValue;
            chunkTracking.mMaxLocation = maxLocation;
            mAssociatedGrains.Add(minLocation.ToString(), chunkTracking);
 
            return Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.None));
        }

        private string[] GetAssociatedGrainIDs()
        {
            string[] ids = new string[mAssociatedGrains.Count];
            mAssociatedGrains.Keys.CopyTo(ids, 0);
            return ids;
        }

        public Task<FeedbackMessage> SetResponseTime(int millisecond)
        {
            mUpdateRateinMilliSeconds = millisecond;
            if (mTimer != null)
            {
                mTimer.Dispose();
                mTimer = RegisterTimer((_) => Fetch(), null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(mUpdateRateinMilliSeconds));
            }

            return Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.None));
        }
    }
}

using Minecraft.OrleansInterfaces;
using Minecraft.OrleansInterfaces.Grains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
namespace Minecraft.OrleansClasses.Grains
{
    public class WorldGrain : Grain, IWorldGrain
    {
        class Tracking
        {
            public IChunkGrain mChunk;
            public IntVec3 mMinPosition;
            public IntVec3 mMaxPosition;
            public Tracking(IChunkGrain chunk, IntVec3 minPosition, IntVec3 maxPosition)
            {
                mChunk = chunk;
                mMinPosition = new IntVec3(minPosition);
                mMaxPosition = new IntVec3(maxPosition);
            }
        }

        Dictionary<Guid, Tracking> mChunkGrains;
        IntVec3 mChunkSize;
        IntVec3 mMaxLocation;
        IntVec3 mMinLocation;
        string mSessionID;

        public override Task OnActivateAsync()
        {
            mChunkGrains = new Dictionary<Guid, Tracking>();
            return base.OnActivateAsync();
        }
        public Task<FeedbackMessage> Create(string sessionID, IntVec3 beginPoint, IntVec3 endPoint, IntVec3 chunkSize, IntVec3 visibilityChunkCount)
        {
            mSessionID = sessionID;
            mChunkSize = chunkSize;
            mMinLocation = beginPoint;
            mMaxLocation = endPoint;
            mChunkGrains.Clear();
            GenerateChunks(beginPoint, endPoint, chunkSize);
            AssociateGrains(visibilityChunkCount, chunkSize);
            return Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.None));
        }


        public Task<WorldInfoResult> GetInfo()
        {
            WorldInfoResult info = new WorldInfoResult();
            info.chunkSize = mChunkSize;
            info.maxLocation = mMaxLocation;
            info.minLocation = mMinLocation;
            return Task.FromResult(info);
        }

        public Task<int> GetNumberOffChunks()
        {
            return Task.FromResult<int>(mChunkGrains.Count);
        }

      

        private void GenerateChunks(IntVec3 beginPoint, IntVec3 endPoint, IntVec3 chunkSize)
        {


            IChunkGrain grainInstance;
            for (int x = beginPoint.x; x <= endPoint.x; x += chunkSize.x)
            {
                for (int y = beginPoint.y; y <= endPoint.y; y += chunkSize.y)
                {
                    for (int z = beginPoint.z; z <= endPoint.z; z += chunkSize.z)
                    {
                        var minPosition = new IntVec3(x, y, z);
                        string id = string.Concat(mSessionID, "#", x, "#", y, "#", z);
                        grainInstance = GrainFactory.GetGrain<IChunkGrain>(id);
                        Guid guid = minPosition.ToGuid();
                        var maxPosition = new IntVec3(minPosition);
                        maxPosition.x += chunkSize.x;
                        maxPosition.y += chunkSize.y;
                        maxPosition.z += chunkSize.z;

                        if (mChunkGrains.ContainsKey(guid) == false)
                        {
                            mChunkGrains.Add(guid, new Tracking(grainInstance, minPosition, maxPosition));
                        }
                        grainInstance.Initialize(id, minPosition, chunkSize);
                    }
                }
            }
        }

        private void AssociateGrains(IntVec3 visibilityChunkCount, IntVec3 chunkSize)
        {
            foreach (var pair in mChunkGrains)
            {
                var pos = pair.Value.mMinPosition;
                var grain = pair.Value.mChunk;
                for (int x = -visibilityChunkCount.x / 2; x < visibilityChunkCount.x / 2; x++)
                {

                    for (int y = -visibilityChunkCount.y / 2; y < visibilityChunkCount.y / 2; y++)
                    {
                        
                        for (int z = -visibilityChunkCount.z / 2; z < visibilityChunkCount.z / 2; z++)
                        {
                             int fidelity = 0;
                            var newPos = new IntVec3((x * chunkSize.x) + pos.x, (y * chunkSize.y) + pos.y, (z * chunkSize.z) + pos.z);
                            Guid id = newPos.ToGuid();
                            if (mChunkGrains.ContainsKey(id) == true)
                            {
                                string grainId = string.Concat(mSessionID, "#", newPos.x, "#", newPos.y, "#", newPos.z);
                                var tracking = mChunkGrains[id];
                                grain.Associate(mSessionID, grainId, fidelity, tracking.mMinPosition, tracking.mMaxPosition);
                            }
                        }
                    }
                }

            }
        }
    }
}

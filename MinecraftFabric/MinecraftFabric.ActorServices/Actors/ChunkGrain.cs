using System.Collections.Generic;
using System.Threading.Tasks;
using Minecraft.OrleansInterfaces;
using Minecraft.OrleansInterfaces.Grains;
using System;
using Orleans;

namespace Minecraft.OrleansClasses.Grains
{
    [Serializable]
    public partial class ChunkGrain : Grain, IChunkGrain
    {
      
        List<IPlayerObserver> mNeedsInitObservers;
        List<PlayerInfo> mActivePlayers;
        List<IPlayerObserver> mObservers;
  
        string mID;
        IntVec3 mMinLocation;
        IntVec3 mMaxLocation;
        IDisposable mTimer;
        int mUpdateRateinMilliSeconds = 30;
        int mBandwidthCap_Players = 1000;
        int mBandwidthCap_Blocks  = 1000;

        public Task<FeedbackMessage> StartPlayer(string playerSessionID, IPlayerObserver playerObserver, Vec3 position, string[] fromGrain)
        {
 
            //Is player already here?
            int idx = mActivePlayers.FindIndex(item => (item.mID == playerSessionID));
            if (idx >= 0)
            {
                return Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.Error, "Already Exist"));
            }

            //Add this player to the active list for other to monitors
            PlayerInfo update = new PlayerInfo();
            update.mPosition = position;
            update.mVersion = MinecraftVersion.GetNext();
            update.mID = playerSessionID;
            mActivePlayers.Add(update);



            //this player want to listen to all associated blocks
            mNeedsInitObservers.Add(playerObserver);

            
            //each associated grain needs to update this one
            foreach (var pair in mAssociatedGrains)
            {
                bool bFound = false;
                if (fromGrain != null)
                {
                    foreach (var id in fromGrain)
                    {
                        if (id == pair.Key)
                        {
                            bFound = true;
                            break;
                        }
                    }
                }

                if (bFound == false)
                {
                    var tracking = pair.Value;
                    tracking.mNeedInitObservers.Add(playerObserver);
                }
            }
         

            //if no updates are occuring, now we have a player to update
            if (mTimer == null)
            {
                mTimer = RegisterTimer((_) => Fetch(), null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(mUpdateRateinMilliSeconds));
            }

            return Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.None));
        }

        public Task<string> UpdatePlayer(string playerSessionID, IPlayerObserver playerObserver, Vec3 position)
        {

            int idx = mActivePlayers.FindIndex(item => (item.mID == playerSessionID));
            if (idx >= 0)
            {

                if ((position.x < mMinLocation.x) || (position.y < mMinLocation.y) || (position.z < mMinLocation.z) ||  
                    (position.x > mMaxLocation.x) || (position.y > mMaxLocation.y) || (position.z > mMaxLocation.z))
                {
                    IChunkGrain transfer = TransferPlayerTo(playerSessionID, playerObserver, ref position, mActivePlayers[idx].mPosition);
                    if (transfer != null)
                    {
                        LeavePlayer(playerSessionID);
                        return Task.FromResult<string>(transfer.GetPrimaryKeyString());
                    }
                }

                mActivePlayers[idx].mPosition = position;
                mActivePlayers[idx].mVersion = MinecraftVersion.GetNext();
                return Task.FromResult<string>(this.GetPrimaryKeyString());
            }


            return Task.FromResult<string>(null);
        }

        
        public Task<FeedbackMessage> LeavePlayer(string playerSessionID)
        {

            int idx = mActivePlayers.FindIndex(item => (item.mID == playerSessionID));
            if (idx >= 0)
            {
                mActivePlayers.RemoveAt(idx);
                mObservers.RemoveAt(idx);
            }
            else
            {
               return Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.Error, "Doesn't Exist"));
            }

            if (mActivePlayers.Count <= 0)
            {
                if (mTimer != null)
                {
                    mTimer.Dispose();
                    mTimer = null;
                }
            }
            return Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.None));

        }


        private IChunkGrain TransferPlayerTo(string playerSessionID, IPlayerObserver playerObserver, ref Vec3 position, Vec3 oldPosition)
        {
            foreach (var pair in mAssociatedGrains)
            {
                var tracking = pair.Value;
                var lowerCorner = tracking.mMinLocation;
                var upperCorner = tracking.mMaxLocation;

                 if ((lowerCorner.x <= position.x) && (lowerCorner.y <= position.y) && (lowerCorner.z <= position.z) &&
                    (upperCorner.x > position.x) && (upperCorner.y > position.y) && (upperCorner.z > position.z))
                {
                    tracking.mGrain.StartPlayer(playerSessionID, playerObserver, position, GetAssociatedGrainIDs());
                    return tracking.mGrain;
                }
            }

            position.Set(oldPosition); 

            return null;
        }



        Task Fetch()
        {
            if (HasLastFetchFinished() == false)
            {
                return TaskDone.Done;
            }

            CalculateDistribution();

            foreach (var pair in mAssociatedGrains)
            {
                var tracking = pair.Value;
                var grain = tracking.mGrain;

                var task = tracking.mUpdateTask;
                if (task != null)
                {
                    var response = task.Result;
                    tracking.mPlayerVersion = response.mLastPlayersVersion;
                    tracking.mBlockVersion = response.mLastBlockVersion;
                }

                if ((tracking.mNeedInitObservers.Count > 0) || (mObservers.Count > 0))
                {
                    tracking.mUpdateTask = grain.InformOfChange(tracking.mNeedInitObservers, mObservers, tracking.mPlayerVersion, tracking.mMaxPlayers, tracking.mBlockVersion, tracking.mMaxBlocks);
                }

                tracking.mNeedInitObservers.Clear();

            }

            mObservers.AddRange(mNeedsInitObservers);
            mNeedsInitObservers.Clear();

            return Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.None));
        }

        private bool HasLastFetchFinished()
        {
            bool ready = true;
            //know how many you want to update...
            foreach (var pair in mAssociatedGrains)
            {
                var tracking = pair.Value;
                var grain = tracking.mGrain;

                //see if the last run is done for this request, and update 
                if (tracking.mUpdateTask != null)
                {
                    var task = tracking.mUpdateTask;
                    if (task.IsCompleted == false)
                    {
                        ready = false;
                    }
                }
            }

            return ready;
        }


        private void CalculateDistribution()
        {
            //need to this chunk first as it is closet to player
            //need to recalculate what we expect out of each chunk
            //now tell the system to update the surrounding
            //know how many you want to update...
            int[] nBlocksLeft = { 0, 0, 0, 0 };
            int[] nPlayersLeft = { 0, 0, 0, 0 };
            float[] blocksPercents = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] playersPercents = { 1.0f, 1.0f, 1.0f, 1.0f };

            const int MaxFidelity = 3;
            foreach (var pair in mAssociatedGrains)
            {
                var tracking = pair.Value;
                //see if the last run is done for this request, and update 
                if (tracking.mUpdateTask != null)
                {
                    var result = tracking.mUpdateTask.Result;
                    if (tracking.mFidelity < MaxFidelity)
                    {
                        nBlocksLeft[tracking.mFidelity] += result.mAvailableBlocks;
                        nPlayersLeft[tracking.mFidelity] += result.mAvailablePlayers;
                    }
                    else
                    {
                        nBlocksLeft[MaxFidelity] += result.mAvailableBlocks;
                        nPlayersLeft[MaxFidelity] += result.mAvailablePlayers;
                    }
                }
            }


            int availablePlayers = mBandwidthCap_Players;
            int availableBlocks = mBandwidthCap_Blocks;
            for (int idx = 0; idx <= MaxFidelity; idx++)
            {
                availablePlayers = FigureCountPerFidelity(nPlayersLeft, playersPercents, idx, MaxFidelity, availablePlayers);
                availableBlocks = FigureCountPerFidelity(nBlocksLeft, blocksPercents, idx, MaxFidelity, availableBlocks);
            }

            //fidelity 0 is always 100%
            playersPercents[0] = 1.0f;
            blocksPercents[0] = 1.0f;

            foreach (var pair in mAssociatedGrains)
            {
                var tracking = pair.Value;
                int index = tracking.mFidelity;
                if (index > MaxFidelity)
                {
                    index = MaxFidelity;
                }

                if (tracking.mUpdateTask != null)
                {
                    var result = tracking.mUpdateTask.Result;

                    tracking.mMaxPlayers = (int)(playersPercents[index] * result.mAvailablePlayers);
                    tracking.mMaxBlocks = (int)(blocksPercents[index] * result.mAvailableBlocks);
                }
                else
                {
                    tracking.mMaxPlayers = (int)(playersPercents[index] * mBandwidthCap_Players);
                    tracking.mMaxBlocks = (int)(blocksPercents[index] * mBandwidthCap_Blocks);
                }

            }
        }

        private int FigureCountPerFidelity(int[] whatLefts, float[] whatPercent, int fidelity, int MaxFidelity, int availablePlayers)
        {
            if ((availablePlayers > 0) && (whatLefts[fidelity] > 0))
            {
                if (whatLefts[fidelity] > availablePlayers)
                {
                    for (int idx = fidelity + 1; idx <= MaxFidelity; idx++)
                    {
                        whatPercent[idx] = 0;
                    }

                    availablePlayers = 0;
                    whatPercent[fidelity] = whatLefts[fidelity] / availablePlayers;
                }
                else
                {
                    whatPercent[fidelity] = 1.0f;
                    availablePlayers -= whatLefts[fidelity];
                }
            }

            return availablePlayers;
        }
    }


}

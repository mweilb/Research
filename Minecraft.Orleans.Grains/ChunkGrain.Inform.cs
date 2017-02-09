using Minecraft.OrleansInterfaces;
using Minecraft.OrleansInterfaces.Grains;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;

namespace Minecraft.OrleansClasses.Grains
{
    public partial class ChunkGrain : Grain, IChunkGrain
    {
        public Task<ResponseToChunkUpdateMessage> InformOfChange(List<IPlayerObserver> initObservers, List<IPlayerObserver> updateObservers, MinecraftVersion lastPlayerVersion, int suggestedMaxPlayerRequests, MinecraftVersion lastBlockVersion, int suggestedMaxBlockRequests)
        {
            //gather new changes from the last delta of changes with the max requested being filled out
            int nBlocksLeft, nPlayersLeft;
            MinecraftVersion lastBlockUsedVersion;
            MinecraftVersion lastPlayerUsedVersion;
            var maxVersion = MinecraftVersion.GetNext();
            GatherChangesAndUpdateObservers(updateObservers, lastBlockVersion,  maxVersion, suggestedMaxBlockRequests, 
                                                             lastPlayerVersion, maxVersion, suggestedMaxPlayerRequests, 
                                                             out nBlocksLeft, out nPlayersLeft,out lastBlockUsedVersion, out lastPlayerUsedVersion);

            //information needed to calculate the ask for next request
            var response = new ResponseToChunkUpdateMessage();
            response.mChunkID = this.mID;
            response.mAvailableBlocks = nBlocksLeft;
            response.mAvailablePlayers = nPlayersLeft;
            response.mLastPlayersVersion = lastPlayerUsedVersion;
            response.mLastBlockVersion = lastBlockUsedVersion;

            //bring all new observers up to the same place as everyone else, but this might not be everything
            if (initObservers.Count > 0)
            {
                GatherChangesAndUpdateObservers(initObservers, MinecraftVersion.MinValue, response.mLastBlockVersion, int.MaxValue, 
                                                               MinecraftVersion.MinValue, response.mLastPlayersVersion, int.MaxValue, out nBlocksLeft, out nPlayersLeft, out lastBlockUsedVersion, out lastPlayerUsedVersion);
            }

            return Task.FromResult<ResponseToChunkUpdateMessage>(response);
        }

        private void GatherChangesAndUpdateObservers(List<IPlayerObserver> observers, 
                                   MinecraftVersion minBlockVersionNumber, MinecraftVersion maxBlockVersionNumber, int suggestedMaxBlocksRequests, 
                                   MinecraftVersion minPlayerVersionNumber, MinecraftVersion maxPlayerVersionNumber, int suggestedMaxPlayerRequests, 
                                   out int nBlocksLeft, out int nPlayersLeft,
                                   out MinecraftVersion lastBlockUsedVersion, out MinecraftVersion lastPlayerUsedVersion)
        {
            int nBlocks = 0; int nPlayers = 0;
            BlockInfo[] blocks = null;
            PlayerInfo[] players = null;
   
            GatherChanges(minBlockVersionNumber, maxBlockVersionNumber, suggestedMaxBlocksRequests, minPlayerVersionNumber, maxPlayerVersionNumber, suggestedMaxPlayerRequests, ref blocks, ref players, out nBlocks, out nPlayers, out nBlocksLeft, out nPlayersLeft, out lastBlockUsedVersion, out lastPlayerUsedVersion);

            //if something to update, tell the observers
            if ((nBlocks != 0) || (nPlayers != 0))
            {
                foreach (var entry in observers)
                {
                    entry.Update(blocks, nBlocks, players, nPlayers);
                }
            }
           
        }

        private void GatherChanges(MinecraftVersion minBlockVersionNumber, MinecraftVersion maxBlockVersionNumber, int suggestedMaxBlockRequests,
                                              MinecraftVersion minPlayerVersionNumber, MinecraftVersion maxPlayerVersionNumber, int suggestedMaxPlayerRequests,
                                              ref BlockInfo[] blocks, ref PlayerInfo[] players,
                                              out int nBlocks, out int nPlayers,
                                              out int nBlocksLeft, out int nPlayersLeft,
                                              out MinecraftVersion lastBlockUsedVersion, out MinecraftVersion lastPlayerUsedVersion)
        {
            int firstIndex = 0;
           
            nBlocks =0;
            var blockRange = new BlockEnumerateRange(mBlocks);

            GetRangeInfo(blockRange, minBlockVersionNumber, maxBlockVersionNumber, suggestedMaxBlockRequests, out firstIndex, out nBlocks,out nBlocksLeft, out lastBlockUsedVersion);        
            if (nBlocks > 0)
            {
                blocks = new BlockInfo[nBlocks];
                mBlocks.CopyTo(firstIndex, blocks, 0, nBlocks);
            }

            nPlayers = 0;
            var playerRange = new PlayersEnumerateRange(mActivePlayers);
            GetRangeInfo(playerRange, minPlayerVersionNumber, maxPlayerVersionNumber, suggestedMaxPlayerRequests, out firstIndex, out nPlayers, out nPlayersLeft, out lastPlayerUsedVersion);
            if (nPlayers > 0)
            {
                players = new PlayerInfo[nPlayers];
                mActivePlayers.CopyTo(firstIndex, players, 0, nPlayers);
            }


        }

      
        private void GetRangeInfo(IEnumrateRange enumerate, MinecraftVersion minVersion, MinecraftVersion maxVersion, int suggestedMax, 
                                         out int firstIndex, out int nItems, out int nItemsLeft, out MinecraftVersion lastUsedVersion)
        {
            int count = 0;
            int lastIndex = -2;

            firstIndex = -1;
            lastUsedVersion = maxVersion;
            MinecraftVersion prevLastVersion = new MinecraftVersion();

            int nEmurates = enumerate.GetCount();
            if (suggestedMax > 0)
            {
                for (int idx = 0; idx < nEmurates; idx++)
                {
                    MinecraftVersion versionItem = enumerate.GetVerison(idx);

                    if ((firstIndex == -1) && (versionItem >= minVersion))
                    {
                        count = 1;
                        firstIndex = idx;
                        lastIndex = idx;
                        prevLastVersion = versionItem;
                    }
                    else if ((firstIndex >= 0) && (versionItem < maxVersion))
                    {
                        count++;
                        lastIndex = idx;
                        //all version must be collected at the same time, even if that over goes the suggested max
                        if ((count > suggestedMax) && (prevLastVersion != versionItem))
                        {
                            lastUsedVersion = versionItem.GetNextTick();
                            lastIndex--;
                            count--;
                            break;
                        }

                    }
                }
            }


            nItems = (lastIndex - firstIndex) + 1;
            nItemsLeft = nEmurates - (lastIndex + 1);
        }

        interface IEnumrateRange
        {
            int GetCount();
            MinecraftVersion GetVerison(int index);
        }

        class BlockEnumerateRange : IEnumrateRange
        {
            List<BlockInfo> mBlocks;
            public BlockEnumerateRange(List<BlockInfo> blocks) { mBlocks = blocks; }
            public int GetCount() { return mBlocks.Count; }
            public MinecraftVersion GetVerison(int index) { return mBlocks[index].mVersion; }
        }

        class PlayersEnumerateRange : IEnumrateRange
        {
            List<PlayerInfo> mPlayers;
            public PlayersEnumerateRange(List<PlayerInfo> players) { mPlayers = players; }
            public int GetCount() { return mPlayers.Count; }
            public MinecraftVersion GetVerison(int index) { return mPlayers[index].mVersion; }
        }
    }
}

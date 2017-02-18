using Microsoft.ServiceFabric.Actors.Runtime;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using MinecraftFabric.Actors.Interfaces;

namespace MinecraftFabric.Actors
{
    public class InformActor : Actor, IInformActor
    {
        public InformActor(ActorService actorService, ActorId actorId) : base(actorService, actorId)
        {
        }

        public async Task<InformOfChunkChangeResponse> InformOfChange(ActorId[] observer, AssociateChunkMetaData associate, TrackingChunkMetaData tracking)
        {

            var maxVersion = MinecraftVersion.GetNext();

            //Get the data associated with request
            var blockPerChunkActor = ActorProxy.Create<BlocksPerChunkActor>(associate.blocksPerChunkActorId, this.ServiceUri);
            var playerPerChunkActor = ActorProxy.Create<PlayersPerChunkActor>(associate.playersPerChunkActorId, this.ServiceUri);
            
            var taskPlayerData = playerPerChunkActor.GetAsync();
            var taskBlockData = blockPerChunkActor.GetAsync();

            await Task.WhenAll(taskPlayerData, taskBlockData);

            var blockData = taskBlockData.Result;
            var playerData = taskPlayerData.Result;


            //gather new changes from the last delta of changes with the max requested being filled out

            GatherChangesAndUpdateObservers(observer, tracking.blockVersion, maxVersion, tracking.maxBlocks,
                                                             tracking.playerVersion, maxVersion, tracking.maxPlayers,
                                                             out int nBlocksLeft, out int nPlayersLeft, out MinecraftVersion lastBlockUsedVersion, out MinecraftVersion lastPlayerUsedVersion,
                                                             blockData, playerData);

            //information needed to calculate the ask for next request
            var response = new InformOfChunkChangeResponse()
            {
                actorID = this.Id,
                availableBlocks = nBlocksLeft,
                availablePlayers = nPlayersLeft,
                lastPlayersVersion = lastPlayerUsedVersion,
                lastBlockVersion = lastBlockUsedVersion
            };

            //bring all new observers up to the same place as everyone else, but this might not be everything
            if (associate.needInitObservers.Count > 0)
            {
                GatherChangesAndUpdateObservers(associate.needInitObservers.ToArray(), MinecraftVersion.minValue, response.lastBlockVersion, int.MaxValue,
                                                                            MinecraftVersion.minValue, response.lastPlayersVersion, int.MaxValue, 
                                                                            out nBlocksLeft, out nPlayersLeft, out lastBlockUsedVersion, out lastPlayerUsedVersion,
                                                                            blockData,playerData);
            }

            return response;
        }

        private void GatherChangesAndUpdateObservers(ActorId[] observers,
                               MinecraftVersion minBlockVersionNumber, MinecraftVersion maxBlockVersionNumber, int suggestedMaxBlocksRequests,
                               MinecraftVersion minPlayerVersionNumber, MinecraftVersion maxPlayerVersionNumber, int suggestedMaxPlayerRequests,
                               out int nBlocksLeft, out int nPlayersLeft,
                               out MinecraftVersion lastBlockUsedVersion, out MinecraftVersion lastPlayerUsedVersion,
                               BlockMetaData[] blocks, PlayerMetaData[] players)
        {
            BlockMetaData[] resultBlocks = null;
            PlayerMetaData[] resultPlayers = null;

            GatherChanges(minBlockVersionNumber,  maxBlockVersionNumber, suggestedMaxBlocksRequests, 
                          minPlayerVersionNumber, maxPlayerVersionNumber, suggestedMaxPlayerRequests,
                          blocks, players, 
                          ref resultBlocks, ref resultPlayers, 
                          out int nBlocks, out int nPlayers, out nBlocksLeft, out nPlayersLeft, 
                          out lastBlockUsedVersion, out lastPlayerUsedVersion);

            //if something to update, tell the observers
            if ((nBlocks != 0) || (nPlayers != 0))
            {
                foreach (var id in observers)
                {
                    var playerAgent = ActorProxy.Create<PlayerAgent>(id, this.ServiceUri);

                    playerAgent.Update(resultBlocks,  resultPlayers);
                }
            }

        }

        private void GatherChanges(MinecraftVersion minBlockVersionNumber, MinecraftVersion maxBlockVersionNumber, int suggestedMaxBlockRequests,
                                              MinecraftVersion minPlayerVersionNumber, MinecraftVersion maxPlayerVersionNumber, int suggestedMaxPlayerRequests,
                                              BlockMetaData[] blocks, PlayerMetaData[] players,
                                              ref BlockMetaData[] blocksResult, ref PlayerMetaData[] playerResult,
                                              out int nBlocks, out int nPlayers,
                                              out int nBlocksLeft, out int nPlayersLeft,
                                              out MinecraftVersion lastBlockUsedVersion, out MinecraftVersion lastPlayerUsedVersion)
        {

            nBlocks = 0;
            var blockRange = new BlockEnumerateRange(blocks);

            GetRangeInfo(blockRange, minBlockVersionNumber, maxBlockVersionNumber, suggestedMaxBlockRequests, out int firstIndex, out nBlocks, out nBlocksLeft, out lastBlockUsedVersion);
            if (nBlocks > 0)
            {
                blocksResult = new BlockMetaData[nBlocks];
                Array.Copy(blocks, firstIndex,blocksResult, 0, nBlocks);
            }

            nPlayers = 0;
            var playerRange = new PlayersEnumerateRange(players);
            GetRangeInfo(playerRange, minPlayerVersionNumber, maxPlayerVersionNumber, suggestedMaxPlayerRequests, out firstIndex, out nPlayers, out nPlayersLeft, out lastPlayerUsedVersion);
            if (nPlayers > 0)
            {
                playerResult = new PlayerMetaData[nPlayers];
                Array.Copy(players,firstIndex, playerResult, 0, nPlayers);
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
            BlockMetaData[] blocks;
            public BlockEnumerateRange(BlockMetaData[] _blocks) { blocks = _blocks; }
            public int GetCount() { return blocks.Length; }
            public MinecraftVersion GetVerison(int index) { return blocks[index].version; }
        }

        class PlayersEnumerateRange : IEnumrateRange
        {
            PlayerMetaData[] players;
            public PlayersEnumerateRange(PlayerMetaData[] _players) { players = _players; }
            public int GetCount() { return players.Length; }
            public MinecraftVersion GetVerison(int index) { return players[index].version; }
        }
    }
}


using System.Threading.Tasks;
using System.Collections.Generic;
using Orleans;

namespace Minecraft.OrleansInterfaces.Grains
{
    public interface IChunkGrain : IGrainWithStringKey
    {
        Task<FeedbackMessage> Initialize(string id, IntVec3 minLocation, IntVec3 maxLocation);
        Task<FeedbackMessage> Associate(string id, string grainID, int fidelity, IntVec3 position, IntVec3 size);
        Task<FeedbackMessage> SetResponseTime(int millisecond);

        Task<FeedbackMessage> StartPlayer(string playerSessionID, IPlayerObserver playerObserver, Vec3 pos, string[] fromGrain);
        Task<string> UpdatePlayer(string playerSessionID, IPlayerObserver playerObserver, Vec3 pos);
        Task<FeedbackMessage> LeavePlayer(string playerSessionID);

        Task<FeedbackMessage> UpdateBlock(string playerSessionID, BlockInfo blockUpdate);

        Task<ResponseToChunkUpdateMessage> InformOfChange(List<IPlayerObserver> initObservers, List<IPlayerObserver> updateObservers, MinecraftVersion lastPlayerVersion, int suggestedMaxPlayerRequests, MinecraftVersion lastBlockVersion, int suggestedMaxBlockRequests);

        //Not for simulation, but for testing
        Task<PlayerInfo[]> InspectPlayers();

        Task<BlockInfo[]> InspectBlocks();


    }
}

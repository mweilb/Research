using System.Threading.Tasks;
using Orleans;

namespace Minecraft.OrleansInterfaces.Grains
{
    public class WorldInfoResult
    {
        public IntVec3 chunkSize;
        public IntVec3 minLocation;
        public IntVec3 maxLocation;  
    };

    public interface IWorldGrain : IGrainWithStringKey
    {
        Task<FeedbackMessage> Create(string sessionID, IntVec3 beginPoint, IntVec3 endPoint, IntVec3 chunkSize, IntVec3 visibilityChunkCount);

        Task<WorldInfoResult> GetInfo();

        Task<int> GetNumberOffChunks();

    }
}

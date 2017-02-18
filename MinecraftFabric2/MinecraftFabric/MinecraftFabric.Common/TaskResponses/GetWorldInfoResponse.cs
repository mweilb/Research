using MinecraftFabric.ActorServices.DataContracts;

namespace MinecraftFabric.ActorServices.TaskResponses
{
    public class GetWorldInfoResponse
    {
        public int stridePerChunks;
        public int visiblityStridePerPlayer;
        public Position minLocation;
        public Position maxLocation;
    }
}

using MinecraftFabric.ActorServices.DataContracts;
using System.Runtime.Serialization;

namespace MinecraftFabrics.ActorServices.DataContracts
{
    [DataContract]
    public class WorldInfoMetaData
    {
        [DataMember]
        public int stridePerChunks;
        [DataMember]
        public int visiblityStridePerPlayer;
        [DataMember]
        public Position minLocation;
        [DataMember]
        public Position maxLocation;
        [DataMember]
        public int chunkCount;

    }
}

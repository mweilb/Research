using MinecraftFabric.Actors.Interfaces;
using System.Runtime.Serialization;

namespace MinecraftFabric.Actors.Interfaces
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

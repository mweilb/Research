using Microsoft.ServiceFabric.Actors;
using System.Runtime.Serialization;

namespace MinecraftFabric.Actors.Interfaces
{
    [DataContract]
    public class PlayerMetaData
    {
        [DataMember]
        public Vector location { get; set; }

        [DataMember]
        public ActorId id { get; set; }

        [DataMember]
        public MinecraftVersion version { get; set; }

    }
}

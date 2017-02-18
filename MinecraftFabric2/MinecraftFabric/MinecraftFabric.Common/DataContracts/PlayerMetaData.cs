using Microsoft.ServiceFabric.Actors;
using MinecraftFabric.ActorServices.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftFabric.ActorServices.DataContracts
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

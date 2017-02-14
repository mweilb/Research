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
        public string id { get; set; }

        [DataMember]
        public MinecraftVersion version { get; set; }

        [DataMember]
        public string creator { get; set; }

        [DataMember]
        public string lastTouched { get; set; }
    }
}

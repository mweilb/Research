using System.Runtime.Serialization;
using System.Text;
using System;

namespace MinecraftFabric.Actors.Interfaces
{
    [DataContract]
    public sealed class BlockMetaData
    {
        [DataMember]
        public Position position { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public Int32 data { get; set; }
        [DataMember]
        public Int32 dataExtended { get; set; }
        [DataMember]
        public MinecraftVersion version { get; set; }

        [DataMember]
        public string creator { get; set; }
        [DataMember]
        public string lastTouched { get; set; }


        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            this.ToJson(sb);

            return sb.ToString();
        }

        public void ToJson(StringBuilder builder)
        {
            builder.AppendFormat(
                "{{ \"position\":{0}, \"id\":{1}, \"version\":{2}, \"data\":{3}, \"dataExtended\":{4}, \"creator\":{5}, \"lastTouched\":{6} }}",
                this.position.ToJson(), this.version, this.data, this.dataExtended, this.creator, this.lastTouched);
        }
    }
}

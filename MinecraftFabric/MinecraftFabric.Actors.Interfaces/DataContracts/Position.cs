using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace MinecraftFabric.Actors.Interfaces
{
    [DataContract]
    public sealed class Position
    {
        [DataMember]
        public int x { get; set; }

        [DataMember]
        public int y { get; set; }

        [DataMember]
        public int z { get; set; }

        public Position()
        {
            Set(0, 0, 0);
        }

        public Position(Position other)
        {
            Set(other);
        }

        public Position(Vector other)
        {
            Set(other);
        }

        public Position(int x, int y, int z)
        {
            Set(x,y,z);
        }

        public void Set(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Set(Position other)
        {
            this.x = other.x;
            this.y = other.y;
            this.z = other.z;
        }

        public void Set(Vector other)
        {
            this.x = Convert.ToInt32(other.x);
            this.y = Convert.ToInt32(other.y);
            this.z = Convert.ToInt32(other.z);
        }

     
        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            this.ToJson(sb);

            return sb.ToString();
        }

        public void ToJson(StringBuilder builder)
        {
            builder.AppendFormat(
                "{{ \"x\":{0}, \"y\":{1}, \"z\":{2} }}",
                this.x.ToString(NumberFormatInfo.InvariantInfo),
                this.y.ToString(NumberFormatInfo.InvariantInfo),
                this.z.ToString(NumberFormatInfo.InvariantInfo));
        }
    }
}

using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System;

namespace MinecraftFabric.ActorServices.DataContracts
{

    [DataContract]
    public sealed class Vector
    {
        [DataMember]
        public double x { get; set; }

        [DataMember]
        public double y { get; set; }

        [DataMember]
        public double z { get; set; }

        public Vector(double x, double y, double z)
        {
            Set(x, y, z);
        }

        public Vector(Vector other)
        {
            Set(other);
        }

        public Vector(Position other)
        {
            Set(other);
        }

        public void Set(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Set(Vector other)
        {
            this.x = other.x;
            this.y = other.y;
            this.z = other.z;
        }

        public void Set(Position other)
        {
            this.x = Convert.ToDouble(other.x);
            this.y = Convert.ToDouble(other.y);
            this.z = Convert.ToDouble(other.z);
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

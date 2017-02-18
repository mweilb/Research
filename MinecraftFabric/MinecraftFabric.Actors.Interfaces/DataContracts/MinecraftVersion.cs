﻿using System;
using System.Runtime.Serialization;
using System.Text;

namespace MinecraftFabric.Actors.Interfaces
{
    [DataContract]
    public sealed class MinecraftVersion
    {
        [DataMember]
        public DateTime id { get; set; }

        public static MinecraftVersion minValue = new MinecraftVersion();

        public MinecraftVersion()
        {
            id = DateTime.MinValue;
        }
        public static MinecraftVersion GetNext()
        {
            MinecraftVersion version = new MinecraftVersion()
            {
                id = DateTime.Now
            };
            return version;
        }

        public MinecraftVersion GetNextTick()
        {
            MinecraftVersion version = new MinecraftVersion()
            {
                id = this.id
            };
            version.id.AddTicks(1);
            return version;
        }
        public static bool operator >(MinecraftVersion x, MinecraftVersion y)
        {
            return (x.id > y.id);
        }

        public static bool operator <(MinecraftVersion x, MinecraftVersion y)
        {
            return (x.id < y.id);
        }

        public static bool operator >=(MinecraftVersion x, MinecraftVersion y)
        {
            return (x.id >= y.id);
        }

        public static bool operator <=(MinecraftVersion x, MinecraftVersion y)
        {
            return (x.id <= y.id);
        }


        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            this.ToJson(sb);

            return sb.ToString();
        }

        public void ToJson(StringBuilder builder)
        {
            builder.AppendFormat( "{{ \"id\":{0}}", this.id);
        }
    }
}

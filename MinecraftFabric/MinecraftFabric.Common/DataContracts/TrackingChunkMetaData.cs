using Microsoft.ServiceFabric.Actors;
using MinecraftFabric.ActorServices.DataContracts;
using MinecraftFabric.ActorServices.TaskResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftFabric.ActorServices.DataContracts
{
    [DataContract]
    public class TrackingChunkMetaData
    {
        [DataMember]
        public int fidelity;
        [DataMember]
        public int maxBlocks;
        [DataMember]
        public MinecraftVersion blockVersion;
        [DataMember]
        public int maxPlayers;
        [DataMember]
        public MinecraftVersion playerVersion;
        [DataMember]
        public Task<InformOfChunkChangeResponse> updateTask = null;

        public TrackingChunkMetaData()
        {
            this.blockVersion = MinecraftVersion.minValue;
            this.playerVersion = MinecraftVersion.minValue;
            this.maxBlocks = 0;
            this.maxPlayers = 0;
            this.updateTask = null;
            this.fidelity = 0;
        }
    }
}

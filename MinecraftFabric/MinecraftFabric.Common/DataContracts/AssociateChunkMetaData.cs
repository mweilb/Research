
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
    public sealed class AssociateChunkMetaData
    {
        public AssociateChunkMetaData(ActorId _actorID, int _fidelity, Position _minLocation, int _blockStride)
        {
            this.actorID = _actorID;
            this.fidelity = _fidelity;
            this.maxBlocks = 0;
            this.maxPlayers = 0;
            this.minLocation = _minLocation;
            this.maxLocation = new Position(_minLocation.x + _blockStride, _minLocation.y + _blockStride, _minLocation.x + _blockStride);
            this.updateTask = null;
            this.playerVersion = MinecraftVersion.GetNext();
            this.stride = _blockStride;
            this.needInitObservers = new List<ActorId>();
        }

        [DataMember]
        public ActorId actorID;
        [DataMember]
        public int fidelity;
        [DataMember]
        public int maxPlayers;
        [DataMember]
        public MinecraftVersion playerVersion;
        [DataMember]
        public int maxBlocks;
        [DataMember]
        public MinecraftVersion blockVersion;
        [DataMember]
        public Position maxLocation;
        [DataMember]
        public Position minLocation;
        [DataMember]
        public int stride;
        [DataMember]
        public List<ActorId> needInitObservers;
        [DataMember]
        public Task<InformOfChunkChangeResponse> updateTask = null;
    }
}

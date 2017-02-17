
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
        public AssociateChunkMetaData(ActorId _informActorId, ActorId _blockPerChunkActorId, ActorId _playersPerChunkActorId,  int _fidelity, Position _minLocation, int _blockStride)
        {
            this.informActorId = _informActorId;
            this.fidelity = _fidelity;
            this.blocksPerChunkActorId = _blockPerChunkActorId;
            this.playersPerChunkActorId = _playersPerChunkActorId;
            this.minLocation = _minLocation;
            this.maxLocation = new Position(_minLocation.x + _blockStride, _minLocation.y + _blockStride, _minLocation.x + _blockStride);
            this.stride = _blockStride;
            this.needInitObservers = new List<ActorId>();
        }

        [DataMember]
        public ActorId informActorId;
        [DataMember]
        public ActorId blocksPerChunkActorId;
        [DataMember]
        public ActorId playersPerChunkActorId;
        [DataMember]
        public int fidelity;
        [DataMember]
        public Position maxLocation;
        [DataMember]
        public Position minLocation;
        [DataMember]
        public int stride;
        [DataMember]
        public List<ActorId> needInitObservers;
    }


}

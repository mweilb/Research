using Microsoft.ServiceFabric.Actors;
using MinecraftFabric.ActorServices.DataContracts;
using MinecraftFabric.ActorServices.Interfaces;
using MinecraftFabric.ActorServices.TaskResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftFabrics.ActorServices.Interface
{
    public interface IInformActor : IActor
    {
        Task<InformOfChunkChangeResponse> InformOfChange(ActorId[] observer, AssociateChunkMetaData associate, TrackingChunkMetaData tracking);
    }
}

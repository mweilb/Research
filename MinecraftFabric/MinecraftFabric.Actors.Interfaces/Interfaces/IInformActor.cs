using Microsoft.ServiceFabric.Actors;
using MinecraftFabric.Actors.Interfaces;
using System.Threading.Tasks;

namespace MinecraftFabric.Actors.Interfaces
{
    public interface IInformActor : IActor
    {
        Task<InformOfChunkChangeResponse> InformOfChange(ActorId[] observer, AssociateChunkMetaData associate, TrackingChunkMetaData tracking);
    }
}

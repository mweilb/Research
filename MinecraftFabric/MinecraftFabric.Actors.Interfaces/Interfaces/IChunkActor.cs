using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace MinecraftFabric.Actors.Interfaces
{
    public interface IChunkActor : IActor
    {
        Task<GenericResponse> Initialize(ActorId worldActorId, Position minLocation, int chunkStride);

        Task<GenericResponse> Associate(ActorId informActorId, ActorId blockPerChunkActorId, ActorId playersPerChunkActorId, int fidelity, Position position, int blockStride);
        Task<ActorId[]> GetAssociations();

        Task<GenericResponse> SetResponseTime(int millisecond);

        Task<GenericResponse> RegisterObserver(ActorId playerAgentID, ActorId[] fromActors);
        
        Task<GenericResponse> UnRegisterObserver(ActorId playerAgentID);

    }
}

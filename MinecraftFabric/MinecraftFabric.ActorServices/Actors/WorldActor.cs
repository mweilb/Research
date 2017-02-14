using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using MinecraftFabric.ActorServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinecraftFabric.ActorServices.DataContracts;
using MinecraftFabric.ActorServices.TaskResponses;
using MinecraftFabrics.ActorServices.DataContracts;
using Microsoft.ServiceFabric.Actors.Client;

namespace MinecraftFabric.ActorServices.Actors
{
    [StatePersistence(StatePersistence.Persisted)]
    class WorldActor : Actor, IWorldActor
    {
        /// <summary>
        /// Initializes a new instance of ActorServices
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public WorldActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "WorldActor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see https://aka.ms/servicefabricactorsstateserialization

            var metaData = new WorldInfoMetaData();
            metaData.stridePerChunks = 0;
            metaData.visiblityStridePerPlayer = 0;
            metaData.chunkCount = 0;
            return this.StateManager.TryAddStateAsync("metaData", metaData);
           
        }

        public Task<GenericResponse> Create(Position beginPoint, Position endPoint, int chunkSize, int visibilityChunkCount)
        {
            var response = new GenericResponse();

            var metaData = new WorldInfoMetaData();
            metaData.maxLocation = endPoint;
            metaData.minLocation = beginPoint;
            metaData.stridePerChunks = chunkSize;
            metaData.visiblityStridePerPlayer = visibilityChunkCount;
            metaData.chunkCount = 0;

            this.StateManager.SetStateAsync<WorldInfoMetaData>("metaData", metaData);
              

     
            return Task.FromResult<GenericResponse>(response);

        }

        public Task<WorldInfoMetaData> GetInfo()
        {
            return this.StateManager.GetStateAsync<WorldInfoMetaData>("metaData"); ;
        }

        private void GenerateChunks(Position beginPoint, Position endPoint, int chunkStride)
        {
            for (int x = beginPoint.x; x <= endPoint.x; x += chunkStride)
            {
                for (int y = beginPoint.y; y <= endPoint.y; y += chunkStride)
                {
                    for (int z = beginPoint.z; z <= endPoint.z; z += chunkStride)
                    {
                        var minPosition = new Position(x, y, z);
                        string guid = string.Concat(this.Id, "#", x, "#", y, "#", z);

                        var chunkActor = ActorProxy.Create<ChunkActor>(new ActorId(guid),this.ServiceUri);
                   
                        var maxPosition = new Position(minPosition);
                        maxPosition.x += chunkStride;
                        maxPosition.y += chunkStride;
                        maxPosition.z += chunkStride;

                        if (mChunkGrains.ContainsKey(guid) == false)
                        {
                            mChunkGrains.Add(guid, new Tracking(grainInstance, minPosition, maxPosition));
                            chunkActor.Initialize(this.Id, minPosition, chunkStride);
                        }

                       

                    }

                }

            }

        }



        private void AssociateGrains(int visibilityChunkCount, Position chunkSize)
        {
            foreach (var pair in mChunkGrains)
            {
                var pos = pair.Value.mMinPosition;
                var grain = pair.Value.mChunk;

                for (int x = -visibilityChunkCount / 2; x < visibilityChunkCount / 2; x++)
                {


             
                    for (int y = -visibilityChunkCount / 2; y < visibilityChunkCount  / 2; y++)

                    {



                        for (int z = -visibilityChunkCount / 2; z < visibilityChunkCount  / 2; z++)

                        {

                            int fidelity = 0;

                            var newPos = new Position((x * chunkSize.x) + pos.x, (y * chunkSize.y) + pos.y, (z * chunkSize.z) + pos.z);

                            string guid = string.Concat(this.Id, "#", newPos.x, "#", newPos.y, "#", newPos.z);
                          

                            if (mChunkGrains.ContainsKey(id) == true)

                            {

                                string grainId = string.Concat(this.Id, "#", newPos.x, "#", newPos.y, "#", newPos.z);

                                var tracking = mChunkGrains[id];

                                grain.Associate(mSessionID, grainId, fidelity, tracking.mMinPosition, tracking.mMaxPosition);

                            }

                        }

                    }

                }



            }

        }


    }
}

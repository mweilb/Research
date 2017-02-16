using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using MinecraftFabric.ActorServices.DataContracts;
using MinecraftFabric.ActorServices.Interfaces;
using MinecraftFabric.ActorServices.TaskResponses;
using System.Collections.Generic;

namespace MinecraftFabric.ActorServices.Actors
{
    class ChunkActor : Actor, IChunkActor
    {
        Dictionary<ActorId, MinecraftVersion> mNeedsInitObservers;
        Dictionary<ActorId, MinecraftVersion> mObservers;
 

        public ChunkActor(ActorService actorService, ActorId actorId) : base(actorService, actorId)
        {
        }

        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "ChunkActor activated.");

            this.StateManager.TryAddStateAsync("minLocation", new Position());
            this.StateManager.TryAddStateAsync("chunkStride", 0);
      
            this.StateManager.TryAddStateAsync("activePlayers", new Dictionary<ActorId, PlayerMetaData>());
            this.StateManager.TryAddStateAsync("activeBlocks", new Dictionary<ActorId, BlockMetaData>());

            return Task.FromResult(0);
        }

        public Task<GenericResponse> Associate(ActorId sessionID, ActorId chunkID, int fidelity, Position position, int blockStride)
        {
            throw new NotImplementedException();
        }

        public Task<InformOfChunkChangeResponse> InformOfChange(IChunkActor actor, MinecraftVersion lastPlayerVersion, int suggestedMaxPlayerRequests, MinecraftVersion lastBlockVersion, int suggestedMaxBlockRequests)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse> Initialize(ActorId sessionID, Position minLocation, int chunkStride)
        {
            throw new NotImplementedException();
        }

        public Task<BlockMetaData[]> InspectBlocks()
        {
            throw new NotImplementedException();
        }

        public Task<PlayerMetaData[]> InspectPlayers()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse> LeavePlayer(ActorId playerAgentID)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse> SetResponseTime(int millisecond)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponse> StartPlayer(ActorId playerAgentID, Vector pos, string[] fromActors)
        {

            var players = await this.StateManager.GetStateAsync<Dictionary<IPlayerAgent, ActorId>>("activePlayers");

            //Is player already here?
            int idx = players.ContainsKey(playerAgentID);
            if (idx >= 0)
            {
                return Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.Error, "Already Exist"));
            }

            //Add this player to the active list for other to monitors
            PlayerInfo update = new PlayerInfo();
            update.mPosition = position;
            update.mVersion = MinecraftVersion.GetNext();
            update.mID = playerSessionID;
            mActivePlayers.Add(update);



            //this player want to listen to all associated blocks
            mNeedsInitObservers.Add(playerObserver);


            //each associated grain needs to update this one
            foreach (var pair in mAssociatedGrains)
            {
                bool bFound = false;
                if (fromGrain != null)
                {
                    foreach (var id in fromGrain)
                    {
                        if (id == pair.Key)
                        {
                            bFound = true;
                            break;
                        }
                    }
                }

                if (bFound == false)
                {
                    var tracking = pair.Value;
                    tracking.mNeedInitObservers.Add(playerObserver);
                }
            }


            //if no updates are occuring, now we have a player to update
            if (mTimer == null)
            {
                mTimer = RegisterTimer((_) => Fetch(), null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(mUpdateRateinMilliSeconds));
            }

            return Task.FromResult<FeedbackMessage>(new FeedbackMessage(FeedbackMessage.Responces.None));
        }

        public Task<GenericResponse> UpdateBlock(ActorId playerAgentID, BlockMetaData blockUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdatePlayer(long playerAgentID, Vector pos)
        {
            throw new NotImplementedException();
        }
    }
}
}

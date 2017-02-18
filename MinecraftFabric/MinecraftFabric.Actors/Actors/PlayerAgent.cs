﻿using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using MinecraftFabric.Actors.Interfaces;
using System.Threading.Tasks;
 

namespace MinecraftFabric.Actors
{

    [StatePersistence(StatePersistence.None)]
    public class PlayerAgent : Actor, IPlayerAgent
    {
        /// <summary>
        /// Initializes a new instance of ActorServices
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public PlayerAgent(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "PlayerAgent activated.");
            return Task.FromResult(0);
        }


        public Task Init(BlockMetaData[] blocks, PlayerMetaData[] players)
        {
            var ev = GetEvent<IChunkUpdateEvent>();
            ev.Init(blocks,players);
            return Task.FromResult(0);
        }

        public Task Update(BlockMetaData[] blocks, PlayerMetaData[] players)
        {
            var ev = GetEvent<IChunkUpdateEvent>();
            ev.Update(blocks, players);
            return Task.FromResult(0);
        }



    }
}

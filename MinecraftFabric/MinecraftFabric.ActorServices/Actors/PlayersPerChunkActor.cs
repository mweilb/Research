﻿using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using MinecraftFabric.ActorServices.DataContracts;
using MinecraftFabric.ActorServices.Interfaces;
using MinecraftFabric.ActorServices.TaskResponses;
using Microsoft.ServiceFabric.Actors.Runtime;
using System.Collections.Generic;

namespace MinecraftFabric.ActorServices.Actors
{
    class PlayersPerChunkActor : Actor, IPlayersPerChunkActor
    {

        const string _playerTag = "players";

        public PlayersPerChunkActor(ActorService actorService, ActorId actorId) : base(actorService, actorId)
        {
        }

        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "playerPerChunkActor activated.");

            this.StateManager.TryAddStateAsync(_playerTag, new Dictionary<ActorId, PlayerMetaData>());

            return Task.FromResult(0);
        }

        public async Task<PlayerMetaData[]> GetAsync()
        {
            var player = await this.StateManager.GetStateAsync<Dictionary<ActorId, PlayerMetaData>>(_playerTag);

            if (player.Count == 0)
            {
                return null;
            }

            var resultplayer = new PlayerMetaData[player.Count];
            player.Values.CopyTo(resultplayer, 0);
            return resultplayer;
        }

        public async Task<GenericResponse> Remove(ActorId playerAgentID)
        {
            var player = await this.StateManager.GetStateAsync<Dictionary<ActorId, PlayerMetaData>>(_playerTag);
            if (player.ContainsKey(playerAgentID) == false)
            {
                return new GenericResponse(false, "Not found");
            }

            player.Remove(playerAgentID);
            await this.StateManager.SetStateAsync(_playerTag, player);
            return new GenericResponse();
        }

        public async Task<GenericResponse> Update(ActorId playerAgentID, PlayerMetaData playerData)
        {
            var player = await this.StateManager.GetStateAsync<Dictionary<ActorId, PlayerMetaData>>(_playerTag);

            playerData.version = MinecraftVersion.GetNext();
            if (player.ContainsKey(playerAgentID))
            {
                player[playerAgentID] = playerData;
            }
            else
            {
                player.Add(playerAgentID, playerData);
            }

            await this.StateManager.SetStateAsync(_playerTag, player);

            return new GenericResponse();
        }
    }
}

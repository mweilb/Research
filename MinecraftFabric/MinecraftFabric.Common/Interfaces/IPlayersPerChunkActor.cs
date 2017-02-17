﻿using Microsoft.ServiceFabric.Actors;
using MinecraftFabric.ActorServices.DataContracts;
using MinecraftFabric.ActorServices.TaskResponses;
using System;
using System.Threading.Tasks;

namespace MinecraftFabric.ActorServices.Interfaces
{
    public interface IPlayersPerChunkActor : IActor
    {
        Task<GenericResponse> Update(ActorId playerAgentID, PlayerMetaData playerMetaData);
        Task<GenericResponse> Remove(ActorId playerAgentID);
        Task<PlayerMetaData[]> GetAsync();
    }
}

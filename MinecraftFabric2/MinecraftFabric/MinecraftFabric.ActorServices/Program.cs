﻿using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors.Runtime;
using MinecraftFabric.ActorServices.Actors;

namespace MinecraftFabric.ActorServices
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                // This line registers an Actor Service to host your actor class with the Service Fabric runtime.
                // The contents of your ServiceManifest.xml and ApplicationManifest.xml files
                // are automatically populated when you build this project.
                // For more information, see https://aka.ms/servicefabricactorsplatform

                ActorRuntime.RegisterActorAsync<ActorServices>(
                   (context, actorType) => new ActorService(context, actorType)).GetAwaiter().GetResult();

                ActorRuntime.RegisterActorAsync<WorldActor>().GetAwaiter().GetResult();
                ActorRuntime.RegisterActorAsync<PlayerAgent>().GetAwaiter().GetResult();
                ActorRuntime.RegisterActorAsync<ChunkActor>().GetAwaiter().GetResult();
                ActorRuntime.RegisterActorAsync<BlocksPerChunkActor>().GetAwaiter().GetResult();
                ActorRuntime.RegisterActorAsync<PlayersPerChunkActor>().GetAwaiter().GetResult();
                ActorRuntime.RegisterActorAsync<InformActor>().GetAwaiter().GetResult();

                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ActorEventSource.Current.ActorHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}

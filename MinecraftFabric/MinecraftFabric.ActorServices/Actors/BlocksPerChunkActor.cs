using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using MinecraftFabric.ActorServices.DataContracts;
using MinecraftFabric.ActorServices.Interfaces;
using MinecraftFabric.ActorServices.TaskResponses;
using Microsoft.ServiceFabric.Actors.Runtime;
using System.Collections.Generic;

namespace MinecraftFabric.ActorServices.Actors
{
    class BlocksPerChunkActor : Actor, IBlocksPerChunkActor
    {

        public BlocksPerChunkActor(ActorService actorService, ActorId actorId) : base(actorService, actorId)
        {
        }

        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "BlocksPerChunkActor activated.");

            this.StateManager.TryAddStateAsync("blocks", new Dictionary<Position, BlockMetaData>());
           
            return Task.FromResult(0);
        }

        public async Task<BlockMetaData[]> GetAsync()
        {
            var blocks = await this.StateManager.GetStateAsync<Dictionary<Position, BlockMetaData>>("blocks");

            if (blocks.Count == 0)
            {
                return null;
            }

            var resultBlocks = new BlockMetaData[blocks.Count];
            blocks.Values.CopyTo(resultBlocks, 0);
            return resultBlocks;
        }

        public async Task<GenericResponse> Remove(ActorId playerAgentID, BlockMetaData blockData)
        {
            var blocks = await this.StateManager.GetStateAsync<Dictionary<Position, BlockMetaData>>("blocks");
            if (blocks.ContainsKey(blockData.position) == false)
            {
                return new GenericResponse(false, "Not found");
            }

            blocks.Remove(blockData.position);
            this.StateManager.SetStateAsync("blocks", blocks);
            return new GenericResponse();
        }

        public async Task<GenericResponse> Update(long playerAgentID, BlockMetaData blockData)
        {
            var blocks = await this.StateManager.GetStateAsync<Dictionary<Position, BlockMetaData>>("blocks");

            blockData.version = MinecraftVersion.GetNext();
            if (blocks.ContainsKey(blockData.position))
            {
                blocks[blockData.position] = blockData;
            }
            else
            {
                blocks.Add(blockData.position, blockData);
            }

            this.StateManager.SetStateAsync("blocks", blocks);

            return new GenericResponse();
        }
    }
}

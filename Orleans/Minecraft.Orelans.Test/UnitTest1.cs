using System;
using System.Threading.Tasks;
using Minecraft.OrleansInterfaces.Grains;
using Orleans;
using Orleans.Runtime.Configuration;
using Orleans.TestingHost;
using Xunit;
using Minecraft.OrleansInterfaces;
using Minecraft.Orleans.Client;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Minecraft.Orleans
{
    /// <summary>
    /// How this works:
    /// 
    /// 1. The class fixture will create an in-process test silo and client
    ///    environment that will be shared by all tests within this class.
    /// 2. The default test environment will contain a mini cluster of 2 silos, 
    ///    running in separate AppDomains from the test process. The silo
    ///    will be named Primary and Secondary_1.
    /// 3. The Orleans client environment will be initialized in the main AppDomain
    ///    where this test class is running, so each of the test cases can assume 
    ///    that everything is initialized at the start of their execution.
    /// 4. The configuration used for the test silos and test client are based on 
    ///    the <c>TestClusterOptions</c> config object which can be configured 
    ///    in the <c>OrleansSiloFixture</c>
    /// 5. There are also various utility methods in the <c>TestCluster</c> class
    ///    that allow silos to be stopped or restarted to allow tests to programmatically 
    ///    simulate some simple failure mode conditions.
    /// 6. Be aware that if you want to have multiple fixtures that create and tear down clusters,
    ///    you should disable parallelization if within the same test project, as it is not possible
    ///    to have 2 different grain clients talking to different silos.
    /// Note: These tests are an example of using xUnit to write unit tests, although
    ///       similar testing frameworks such as NUnit or MsTest could have been used.
    ///       Consider using a collection fixture (using <see cref="ICollectionFixture{TFixture}"/> as 
    ///       opposed to implementing <see cref="IClassFixture{TFixture}"/> in the test class) if you want
    ///       to reuse the same cluster accross many test classes.
    /// </summary>
    public class BaseFunctional : IClassFixture<OrleansSiloFixture>
    {
        private readonly OrleansSiloFixture _fixture;

        private MinecraftOrleansWorldClient _worldClient;
        public BaseFunctional(OrleansSiloFixture fixture)
        {
            _fixture = fixture;
  
        }

        public Task<FeedbackMessage> CreateWorld(string sessionID, IntVec3 beginPoint, IntVec3 endPoint, IntVec3 chunkSize, IntVec3 visibilityChunkCount)
        {
            var worldGrain = GrainClient.GrainFactory.GetGrain<IWorldGrain>(sessionID);
            var task = worldGrain.Create(sessionID, beginPoint, endPoint, chunkSize, visibilityChunkCount);
            Thread.Sleep(1000);
            return task;
        }
        //private IGrainFactory GrainFactory => _fixture.Cluster.GrainFactory;

        public class PlayerObserver : IPlayerObserver
        {
            public class Tracking
            {
                public BlockInfo[] Blocks;
                public int nBlocks;
                public PlayerInfo[] Players;
                public int nPlayers;
            }

            public List<Tracking> mUpdates = new List<Tracking>();
            public void Update(BlockInfo[] blocks, int nBlocks, PlayerInfo[] players, int nPlayers)
            {
                var track = new Tracking();
                track.Blocks = blocks;
                track.nBlocks = nBlocks;
                track.Players = players;
                track.nPlayers = nPlayers;
                mUpdates.Add(track);
            }
 
        }

        [Fact]
        public async Task Validate_MinecraftVersion()
        {
            MinecraftVersion v1 = MinecraftVersion.GetNext();
            Thread.Sleep(TimeSpan.FromMilliseconds(1));
            MinecraftVersion v2 = MinecraftVersion.GetNext();

            bool bGreaterThan = (v1 < v2);
            bool bLessThan = (v2 > v1);
            bool bGreaterEqualThan = (v1 <= v2);
            bool bLessEqualThan = (v2 >= v1);

            Assert.Equal(true, bGreaterEqualThan);
            Assert.Equal(true, bLessEqualThan);
            Assert.Equal(true, bLessThan);
            Assert.Equal(true, bGreaterThan);

        }

        [Fact]
        public async Task SiloCreateWorldAndOneChunk()
        {
            string _sessionID = "SiloCreateWorldAndOneChunk";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 15, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            var result = await _worldClient.Initialize(_sessionID);
            int chunks = await _worldClient.GetNumberOffChunks();
            Assert.Equal(1, chunks);
        }

        [Fact]
        public async Task SiloCreateWorld_64By64()
        {
            string _sessionID = "SiloCreateWorld_64By64";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(63, 63, 63), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            int chunks = await _worldClient.GetNumberOffChunks();
            Assert.Equal(64, chunks);
        }

        [Fact] async Task SiloAddOnePlayer()
        {
            string _sessionID = "SiloAddOnePlayer";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 16, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            Thread.Sleep(500);

            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();

            var spawnPoint = new Vec3(0.0f, 4.0f, 0.0f);
            await player.Initialize(_worldClient, "me", watcher, spawnPoint);

            Thread.Sleep(120);
            if (watcher.mUpdates.Count >= 1)
            {
                Assert.Equal(1, watcher.mUpdates[0].nPlayers);
                Assert.Equal(spawnPoint.x, watcher.mUpdates[0].Players[0].mPosition.x);
                Assert.Equal(spawnPoint.y, watcher.mUpdates[0].Players[0].mPosition.y);
                Assert.Equal(spawnPoint.z, watcher.mUpdates[0].Players[0].mPosition.z);
            }
           
        }


        [Fact]
        async Task SiloAddThreePlayers()
        {
            string _sessionID = "SiloAddThreePlayers";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 15, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            Thread.Sleep(500);

            var player1 = new MinecraftOrleansPlayerClient();
            var player2 = new MinecraftOrleansPlayerClient();
            var player3 = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher1 = new PlayerObserver();
            PlayerObserver watcher2 = new PlayerObserver();
            PlayerObserver watcher3 = new PlayerObserver();

            var spawnPoint1 = new Vec3(0.0f, 4.0f, 0.0f);
            var spawnPoint2 = new Vec3(0.0f, 0.0f, 4.0f);
            var spawnPoint3 = new Vec3(4.0f, 0.0f, 0.0f);
            await player1.Initialize(_worldClient, "one", watcher1, spawnPoint1);
            await player2.Initialize(_worldClient, "two", watcher2, spawnPoint2);
            await player3.Initialize(_worldClient, "three", watcher3, spawnPoint3);

            Thread.Sleep(180);
            int nPlayer = 0;
            foreach (var update in watcher1.mUpdates)
            {
                nPlayer += update.nPlayers;
            }
            
            Assert.Equal(3, nPlayer);
       
        }

        [Fact]
        async Task SiloAddOnePlayer_Resheduled()
        {
            string _sessionID = "SiloAddOnePlayer_Resheduled";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 16, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            Thread.Sleep(500);

            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();

            var spawnPoint = new Vec3(0.0f, 4.0f, 0.0f);
            await player.Initialize(_worldClient, "me", watcher, spawnPoint);
            player.TESTONLYCALL_SETRESPONSETIME(1);

            var updatePath = new Vec3(10.0f, 10.0f, 10.0f);
            var feedback = await player.Update(updatePath);
            Thread.Sleep(260);
            feedback = await player.Update(updatePath);
            Thread.Sleep(260);
            if (watcher.mUpdates.Count > 1)
            {
                Assert.Equal(1, watcher.mUpdates[1].nPlayers);
                Assert.Equal(updatePath.x, watcher.mUpdates[1].Players[0].mPosition.x);
                Assert.Equal(updatePath.y, watcher.mUpdates[1].Players[0].mPosition.y);
                Assert.Equal(updatePath.z, watcher.mUpdates[1].Players[0].mPosition.z);
            }

        }

        [Fact]
        async Task ValidateError_DuplicatePlayerAdds()
        {
            string _sessionID = "ValidateError_DuplicatePlayerAdds";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 16, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            Thread.Sleep(500);

            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();

            var spawnPoint = new Vec3(0.0f, 4.0f, 0.0f);
            var feedback = await player.Initialize(_worldClient, "me", watcher, spawnPoint);
            Assert.Equal(FeedbackMessage.Responces.None, feedback.mType);

            feedback = await player.Initialize(_worldClient, "me", watcher, spawnPoint);
            Assert.Equal(FeedbackMessage.Responces.Error, feedback.mType);

        }

        [Fact]
        async Task SiloAddPlayer_ValidateSlowPath()
        {
            string _sessionID = "SiloAddPlayer_ValidateSlowPath";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 16, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);


            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();

            var spawnPoint = new Vec3(0.0f, 4.0f, 0.0f);
            await player.Initialize(_worldClient, "me", watcher, spawnPoint);
            var players = await _worldClient.InspectPlayers(new IntVec3(0, 0, 0));


            Assert.Equal(1, players.Length);
            Assert.Equal(spawnPoint.x, players[0].mPosition.x);
            Assert.Equal(spawnPoint.y, players[0].mPosition.y);
            Assert.Equal(spawnPoint.z, players[0].mPosition.z);
        }

        [Fact]
        async Task SiloAddBlock_ValidateSlowPath()
        {
            string _sessionID = "SiloAddBlock_ValidateSlowPath";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 16, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);



            BlockInfo blockInfo = new BlockInfo();
            blockInfo.mID = "Grass";
            blockInfo.mPosition = new IntVec3(0, 1, 0);
            blockInfo.mData = 128;
            blockInfo.mDataExtended = 256;
         
            await _worldClient.UpdateBlock("me",blockInfo);

        
            var blocks = await _worldClient.InspectBlocks(new IntVec3(0, 0, 0));


            Assert.Equal(1, blocks.Length);
            Assert.Equal(blockInfo.mPosition.x, blocks[0].mPosition.x);
            Assert.Equal(blockInfo.mPosition.y, blocks[0].mPosition.y);
            Assert.Equal(blockInfo.mPosition.z, blocks[0].mPosition.z);
        }

        [Fact]
        async Task ValidateError_BlockBadInfo()
        {
            string _sessionID = "ValidateError_BlockBadInfo";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 16, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            var feedback = await _worldClient.UpdateBlock("me", null);

            Assert.Equal(FeedbackMessage.Responces.Error, feedback.mType);
        }


        [Fact]
        async Task ValidateError_BlockBadPosition()
        {
            string _sessionID = "ValidateError_BlockBadInfo";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 16, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            BlockInfo blockInfo = new BlockInfo();
            blockInfo.mID = "Grass";
            blockInfo.mPosition = new IntVec3(0, 64, 0);
            blockInfo.mData = 128;
            blockInfo.mDataExtended = 256;

            var feedback = await _worldClient.UpdateBlock("me", blockInfo);

            Assert.Equal(FeedbackMessage.Responces.Error, feedback.mType);
        }

        [Fact]
        async Task ValidateError_BlockNotInitialized()
        {
            string _sessionID = "ValidateError_BlockNotInitialized";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 16, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
    
            BlockInfo blockInfo = new BlockInfo();
            blockInfo.mID = "Grass";
            blockInfo.mPosition = new IntVec3(0, 64, 0);
            blockInfo.mData = 128;
            blockInfo.mDataExtended = 256;

            var feedback = await _worldClient.UpdateBlock("me", blockInfo);

            Assert.Equal(FeedbackMessage.Responces.Error, feedback.mType);
        }

        [Fact]
        async Task SiloAddBlock_PushPath()
        {
            string _sessionID = "SiloAddBlock_PushPath";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 115, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);



            BlockInfo blockInfo = new BlockInfo();
            blockInfo.mID = "Grass";
            blockInfo.mPosition = new IntVec3(0, 1, 0);
            blockInfo.mData = 128;
            blockInfo.mDataExtended = 256;

            await _worldClient.UpdateBlock("me", blockInfo);

            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();

            var spawnPoint = new Vec3(0.0f, 4.0f, 0.0f);
            await player.Initialize(_worldClient, "me", watcher, spawnPoint);

            Thread.Sleep(60);
            Assert.Equal(1, watcher.mUpdates.Count);
            var update = watcher.mUpdates[0];
            Assert.Equal(1, update.nPlayers);
            Assert.Equal(spawnPoint.x, update.Players[0].mPosition.x);
            Assert.Equal(spawnPoint.y, update.Players[0].mPosition.y);
            Assert.Equal(spawnPoint.z, update.Players[0].mPosition.z);

            Assert.Equal(1, update.nBlocks);
            Assert.Equal(blockInfo.mPosition.x, update.Blocks[0].mPosition.x);
            Assert.Equal(blockInfo.mPosition.y, update.Blocks[0].mPosition.y);
            Assert.Equal(blockInfo.mPosition.z, update.Blocks[0].mPosition.z);
        }


        [Fact]
        async Task UpdateSameBlock_PushPath()
        {
            string _sessionID = "UpdateSameBlock_PushPath";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 115, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);



            BlockInfo blockInfo = new BlockInfo();
            blockInfo.mID = "Grass";
            blockInfo.mPosition = new IntVec3(0, 1, 0);
            blockInfo.mData = 128;
            blockInfo.mDataExtended = 256;

            await _worldClient.UpdateBlock("me", blockInfo);

            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();

            var spawnPoint = new Vec3(0.0f, 4.0f, 0.0f);
            await player.Initialize(_worldClient, "me", watcher, spawnPoint);

            Thread.Sleep(120);
            Assert.Equal(1, watcher.mUpdates.Count);
            var update = watcher.mUpdates[0];
            Assert.Equal(1, update.nPlayers);
            Assert.Equal(spawnPoint.x, update.Players[0].mPosition.x);
            Assert.Equal(spawnPoint.y, update.Players[0].mPosition.y);
            Assert.Equal(spawnPoint.z, update.Players[0].mPosition.z);

            Assert.Equal(1, update.nBlocks);
            Assert.Equal(blockInfo.mPosition.x, update.Blocks[0].mPosition.x);
            Assert.Equal(blockInfo.mPosition.y, update.Blocks[0].mPosition.y);
            Assert.Equal(blockInfo.mPosition.z, update.Blocks[0].mPosition.z);

            blockInfo.mData = 0x80;
            await _worldClient.UpdateBlock("me", blockInfo);
            Thread.Sleep(120);

            Assert.Equal(2, watcher.mUpdates.Count);
            update = watcher.mUpdates[1];
            Assert.Equal(blockInfo.mPosition.x, update.Blocks[0].mPosition.x);
            Assert.Equal(blockInfo.mPosition.y, update.Blocks[0].mPosition.y);
            Assert.Equal(blockInfo.mPosition.z, update.Blocks[0].mPosition.z);
            Assert.Equal(blockInfo.mData, update.Blocks[0].mData);

        }

        [Fact]
        async Task AddTwoBlocks_PushPath()
        {
            string _sessionID = "SiloAddTwoBlocks_PushPath";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 115, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);



            BlockInfo blockInfo = new BlockInfo();
            blockInfo.mID = "Grass";
            blockInfo.mPosition = new IntVec3(0, 1, 0);
            blockInfo.mData = 128;
            blockInfo.mDataExtended = 256;

            await _worldClient.UpdateBlock("me", blockInfo);

            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();

            var spawnPoint = new Vec3(0.0f, 4.0f, 0.0f);
            await player.Initialize(_worldClient, "me", watcher, spawnPoint);

            Thread.Sleep(250);
            Assert.Equal(1, watcher.mUpdates.Count);
            var update = watcher.mUpdates[0];
            Assert.Equal(1, update.nPlayers);
            Assert.Equal(spawnPoint.x, update.Players[0].mPosition.x);
            Assert.Equal(spawnPoint.y, update.Players[0].mPosition.y);
            Assert.Equal(spawnPoint.z, update.Players[0].mPosition.z);

            Assert.Equal(1, update.nBlocks);
            Assert.Equal(blockInfo.mPosition.x, update.Blocks[0].mPosition.x);
            Assert.Equal(blockInfo.mPosition.y, update.Blocks[0].mPosition.y);
            Assert.Equal(blockInfo.mPosition.z, update.Blocks[0].mPosition.z);

            blockInfo.mPosition = new IntVec3(1, 1, 1);
            blockInfo.mData = 0x80;
            await _worldClient.UpdateBlock("me", blockInfo);
            Thread.Sleep(120);

            Assert.Equal(2, watcher.mUpdates.Count);
            update = watcher.mUpdates[1];
            Assert.Equal(blockInfo.mPosition.x, update.Blocks[0].mPosition.x);
            Assert.Equal(blockInfo.mPosition.y, update.Blocks[0].mPosition.y);
            Assert.Equal(blockInfo.mPosition.z, update.Blocks[0].mPosition.z);
            Assert.Equal(blockInfo.mData, update.Blocks[0].mData);

        }



        [Fact]
        async Task ValidateError_SpawnOutsideWorld()
        {
            string _sessionID = "ValidateError_SpawnOutsideWorld";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 15, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();

            var spawnPoint = new Vec3(40.0f, 40.0f, 40.0f);
            var feedback = await player.Initialize(_worldClient, "me", watcher, spawnPoint);

            Assert.Equal(FeedbackMessage.Responces.Error, feedback.mType); 
           
        }

        [Fact]
        async Task ValidateError_UpdateBeforeInit()
        {
            string _sessionID = "ValidateError_UpdateBeforeInit";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 16, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();

            var spawnPoint = new Vec3(40.0f, 40.0f, 40.0f);
            var feedback = await player.Update(spawnPoint);

            Assert.Equal(FeedbackMessage.Responces.Error, feedback.mType);

        }

        [Fact]
        async Task ValidateError_LeaveBeforStarting_ClientCheck()
        {
            string _sessionID = "ValidateError_LeaveBeforStarting_ClientCheck";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 16, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();

            var feedback = await player.Leave();

            Assert.Equal(FeedbackMessage.Responces.Error, feedback.mType);

        }

        [Fact]
        public async Task Validate_InvalidPlayerDuringUpdate()
        {

            string _sessionID = "Validate_InvalidPlayerDuringUpdate";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 32, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            // The Orleans silo / client test environment is already set up at this point.
            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();

            var pos1 = new Vec3(0.0f, 4.0f, 0.0f);
            await player.Initialize(_worldClient, "me", watcher, pos1);
            Thread.Sleep(240);
            Assert.Equal(1, watcher.mUpdates.Count);
            Assert.Equal(1, watcher.mUpdates[0].nPlayers);
            Assert.Equal(pos1.x, watcher.mUpdates[0].Players[0].mPosition.x);
            Assert.Equal(pos1.y, watcher.mUpdates[0].Players[0].mPosition.y);
            Assert.Equal(pos1.z, watcher.mUpdates[0].Players[0].mPosition.z);

            var pos2 = new Vec3(0.0f, 4.0f, 0.0f);
            player.TESTONLYCALL_CHANGEPLAYERNAME("not me");

            var feedback = await player.Update(pos2);
            Assert.Equal(FeedbackMessage.Responces.Error, feedback.mType);



            //Assert.Equal($"You said: '{greeting}', I say: Hello!", reply);
        }




        [Fact]
        async Task ValidateError_LeaveBeforStarting_ServerCheck()
        {
            string _sessionID = "ValidateError_LeaveBeforStarting_ServerCheck";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 16, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();
      
            var spawnPoint = new Vec3(0.0f, 0.0f, 0.0f);
            var feedback = await player.Initialize(_worldClient, "me", watcher, spawnPoint);

            player.TESTONLYCALL_CHANGEPLAYERNAME("CHANGED");
            feedback = await player.Leave();

            Assert.Equal(FeedbackMessage.Responces.Error, feedback.mType);

        }

        [Fact]
        public async Task SiloHelloPlayer()
        {

            string _sessionID = "SiloHelloPlayer";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 16, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            // The Orleans silo / client test environment is already set up at this point.
            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();

            var pos1 = new Vec3(0.0f, 4.0f, 0.0f);
            await player.Initialize(_worldClient, "me", watcher, pos1);
            Thread.Sleep(60);
            Assert.Equal(1, watcher.mUpdates.Count);
            Assert.Equal(1, watcher.mUpdates[0].nPlayers);
            Assert.Equal(pos1.x, watcher.mUpdates[0].Players[0].mPosition.x);
            Assert.Equal(pos1.y, watcher.mUpdates[0].Players[0].mPosition.y);
            Assert.Equal(pos1.z, watcher.mUpdates[0].Players[0].mPosition.z);

            var pos2 = new Vec3(0.0f, 5.0f, 0.0f);
            await player.Update(pos2);
            Thread.Sleep(60);
            Assert.Equal(2, watcher.mUpdates.Count);
            Assert.Equal(1, watcher.mUpdates[0].nPlayers);
            Assert.Equal(pos2.x, watcher.mUpdates[1].Players[0].mPosition.x);
            Assert.Equal(pos2.y, watcher.mUpdates[1].Players[0].mPosition.y);
            Assert.Equal(pos2.z, watcher.mUpdates[1].Players[0].mPosition.z);

            var pos3 = new Vec3(0.0f, 6.0f, 0.0f);
            await player.Update(pos3);
            Thread.Sleep(60);
            Assert.Equal(3, watcher.mUpdates.Count);
            Assert.Equal(1, watcher.mUpdates[0].nPlayers);
            Assert.Equal(pos3.x, watcher.mUpdates[2].Players[0].mPosition.x);
            Assert.Equal(pos3.y, watcher.mUpdates[2].Players[0].mPosition.y);
            Assert.Equal(pos3.z, watcher.mUpdates[2].Players[0].mPosition.z);

            player.Leave();
            //Assert.Equal($"You said: '{greeting}', I say: Hello!", reply);
        }

        [Fact]
        public async Task MovePlayerToAnotherChunk()
        {

            string _sessionID = "MovePlayerToAnotherChunk";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 32, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);
 
            // The Orleans silo / client test environment is already set up at this point.
            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();

            var pos1 = new Vec3(0.0f, 4.0f, 0.0f);
            await player.Initialize(_worldClient, "me", watcher, pos1);
            Thread.Sleep(240);
            Assert.Equal(1, watcher.mUpdates.Count);
            Assert.Equal(1, watcher.mUpdates[0].nPlayers);
            Assert.Equal(pos1.x, watcher.mUpdates[0].Players[0].mPosition.x);
            Assert.Equal(pos1.y, watcher.mUpdates[0].Players[0].mPosition.y);
            Assert.Equal(pos1.z, watcher.mUpdates[0].Players[0].mPosition.z);

            var pos2 = new Vec3(0.0f, 18.0f, 0.0f);
            await player.Update(pos2);
            Thread.Sleep(240);
            Assert.Equal(2, watcher.mUpdates.Count);
            Assert.Equal(1, watcher.mUpdates[1].nPlayers);
            Assert.Equal(pos2.x, watcher.mUpdates[1].Players[0].mPosition.x);
            Assert.Equal(pos2.y, watcher.mUpdates[1].Players[0].mPosition.y);
            Assert.Equal(pos2.z, watcher.mUpdates[1].Players[0].mPosition.z);
           

            player.Leave();
            //Assert.Equal($"You said: '{greeting}', I say: Hello!", reply);
        }


        [Fact]
        public async Task MovePlayerToOutsideWorld()
        {

            string _sessionID = "MovePlayerToOutsideWorld";
            await CreateWorld(_sessionID, new IntVec3(0, 0, 0), new IntVec3(0, 32, 0), new IntVec3(16, 16, 16), new IntVec3(16, 16, 16));

            _worldClient = new MinecraftOrleansWorldClient();
            await _worldClient.Initialize(_sessionID);

            // The Orleans silo / client test environment is already set up at this point.
            var player = new MinecraftOrleansPlayerClient();
            PlayerObserver watcher = new PlayerObserver();

            var pos1 = new Vec3(0.0f, 4.0f, 0.0f);
            await player.Initialize(_worldClient, "me", watcher, pos1);
            Thread.Sleep(240);
            Assert.Equal(1, watcher.mUpdates.Count);
            Assert.Equal(1, watcher.mUpdates[0].nPlayers);
            Assert.Equal(pos1.x, watcher.mUpdates[0].Players[0].mPosition.x);
            Assert.Equal(pos1.y, watcher.mUpdates[0].Players[0].mPosition.y);
            Assert.Equal(pos1.z, watcher.mUpdates[0].Players[0].mPosition.z);

            var pos2 = new Vec3(0.0f, 64.0f, 0.0f);
            await player.Update(pos2);
            Thread.Sleep(240);
            Assert.Equal(2, watcher.mUpdates.Count);
            Assert.Equal(1, watcher.mUpdates[1].nPlayers);
            Assert.Equal(pos1.x, watcher.mUpdates[1].Players[0].mPosition.x);
            Assert.Equal(pos1.y, watcher.mUpdates[1].Players[0].mPosition.y);
            Assert.Equal(pos1.z, watcher.mUpdates[1].Players[0].mPosition.z);


            player.Leave();
            //Assert.Equal($"You said: '{greeting}', I say: Hello!", reply);
        }

    }

    /// <summary>
    /// Class fixture used to share the silos between multiple tests within a specific test class.
    /// </summary>
    public class OrleansSiloFixture : IDisposable
    {
        public TestCluster Cluster { get; }

        public OrleansSiloFixture()
        {
            GrainClient.Uninitialize();

            var options = new TestClusterOptions(initialSilosCount: 1);
            options.ClusterConfiguration.AddMemoryStorageProvider("Default");
            options.ClusterConfiguration.AddMemoryStorageProvider("MemoryStore");
            Cluster = new TestCluster(options);

            if (Cluster.Primary == null)
            {
                Cluster.Deploy();
            }
        }

       

        /// <summary>
        /// Clean up the test fixture once all the tests have been run
        /// </summary>
        public void Dispose()
        {
            Cluster.StopAllSilos();
        }
    }
}

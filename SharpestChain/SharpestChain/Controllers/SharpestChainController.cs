namespace Com.Innoq.SharpestChain.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;

    using IO;

    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;
        
    using System.Threading.Tasks;

    using Akka.Actor;

    using Blocks;

    using data;

    using Data;

    using Eventing;

    using reservieren.Controllers;

    [Route("/")]
    public class SharpestChainController : Controller
    {
      
        private readonly string _nodeId = Guid.NewGuid().ToString();
        
        private readonly IEventConnectionHolderActorRef _connectionHolderActorRef;

        private readonly ISharpestChainPersistenceActorRef _persistenceActorRef;

        public SharpestChainController(IEventConnectionHolderActorRef pConnectionHolderActorRef, ISharpestChainPersistenceActorRef persistenceActorRef)
        {
            _connectionHolderActorRef = pConnectionHolderActorRef;
            _persistenceActorRef = persistenceActorRef;
        }

        // GET /
        [HttpGet]
        public async Task<IActionResult> NodeInfo()
        {
            var nodeInfo = new NodeInfo
                           {
                                   NodeId = _nodeId,
                                   CurrentBlockHeight = _persistenceActorRef.GetActorRef().Ask<ReadOnlyCollection<Block>>(new SharpestChainPersisrtence_Messages.GetBlocks(), TimeSpan.FromSeconds(5)).Result.Last().Index
                           };
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(nodeInfo));
            var stream = new MemoryStream(byteArray);
            
            return await Task.FromResult(new FileStreamResult(stream, "application/json"));
        }

        // GET blocks
        [HttpGet("blocks")]
        public async Task<IActionResult> Blocks()
        {
           var blocks = _persistenceActorRef.GetActorRef().Ask<ReadOnlyCollection<Block>>(new SharpestChainPersisrtence_Messages.GetBlocks(), TimeSpan.FromSeconds(5)).Result; 
           var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(blocks));
           var stream = new MemoryStream(byteArray);
            
           return await Task.FromResult(new FileStreamResult(stream, "application/json"));
        }

        // GET mine
        [HttpGet("mine")]
        public async Task<IActionResult> Mine()
        {
           var blocks = _persistenceActorRef.GetActorRef().Ask<ReadOnlyCollection<Block>>(new SharpestChainPersisrtence_Messages.GetBlocks(), TimeSpan.FromSeconds(5)).Result;
            
           var block = Miner.BlockFinder(blocks.Last());
            
            _persistenceActorRef.GetActorRef().Tell(block);
              
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(block));
            var stream = new MemoryStream(byteArray);
            
            return await Task.FromResult(new FileStreamResult(stream, "application/json"));
            
            
        }
        
        [HttpGet("events")]
        public IActionResult Events() => new PushActorStreamResult(_connectionHolderActorRef, "text/event-stream", _persistenceActorRef);
    }
}

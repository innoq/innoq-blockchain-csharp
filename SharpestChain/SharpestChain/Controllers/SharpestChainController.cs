namespace Com.Innoq.SharpestChain.Controllers
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Akka.Actor;

    using Blocks;

    using Data;

    using Eventing;

    using IO;

    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    using reservieren.Controllers;

    [Route("/")]
    public class SharpestChainController : Controller
    {
        private readonly string _nodeId = Guid.NewGuid().ToString();

        private readonly IEventConnectionHolderActorRef _connectionHolderActorRef;

        private readonly IPersistenceActorRef _persistenceActorRef;

        public SharpestChainController(IEventConnectionHolderActorRef pConnectionHolderActorRef,
                                       IPersistenceActorRef persistenceActorRef)
        {
            _connectionHolderActorRef = pConnectionHolderActorRef;
            _persistenceActorRef = persistenceActorRef;
        }

        // GET /
        [HttpGet]
        public IActionResult NodeInfo()
        {
            var nodeInfo = new NodeInfo
                           {
                                   NodeId = _nodeId,

                                   CurrentBlockHeight =
                                           _persistenceActorRef
                                                   .GetActorRef()
                                                   .Ask<ReadOnlyCollection<Block>>(
                                                           new Persistence.GetBlocks(),
                                                           TimeSpan.FromSeconds(5)).Result.Last().Index
                           };

            return Content(JsonConvert.SerializeObject(nodeInfo), "application/json", Encoding.UTF8);
        }

        // GET blocks
        [HttpGet("blocks")]
        public IActionResult Blocks()
        {
            var blocks = _persistenceActorRef.GetActorRef()
                                             .Ask<ReadOnlyCollection<Block>>(
                                                     new Persistence.GetBlocks(),
                                                     TimeSpan.FromSeconds(5)).Result;

            return Content(JsonConvert.SerializeObject(blocks), "application/json", Encoding.UTF8);
        }

        // GET mine
        [HttpGet("mine")]
        public IActionResult Mine()
        {
            var blocks = _persistenceActorRef.GetActorRef()
                                             .Ask<ReadOnlyCollection<Block>>(
                                                     new Persistence.GetBlocks(),
                                                     TimeSpan.FromSeconds(5)).Result;

            var block = Miner.FindNewBlock(blocks.Last());

            _persistenceActorRef.GetActorRef().Tell(block);

            return Content(block.toJson(), "application/json", Encoding.UTF8);
        }

        [HttpGet("events")]
        public IActionResult Events()
            => new PushActorStreamResult(_connectionHolderActorRef, "text/event-stream", _persistenceActorRef);
    }
}

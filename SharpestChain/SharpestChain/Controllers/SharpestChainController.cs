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

    using Util;

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
            var block = Miner.FindNewBlock(_persistenceActorRef);

            _persistenceActorRef.GetActorRef().Tell(block);

            return Content(block.toJson(), "application/json", Encoding.UTF8);
        }

        [HttpGet("events")]
        public IActionResult Events()
        {
            return new PushActorStreamResult(_connectionHolderActorRef, "text/event-stream", _persistenceActorRef);
        }

        [HttpGet("transactions")]
        public IActionResult GetTransactions()
        {
            var transactions = _persistenceActorRef.GetActorRef()
                                                   .Ask<ReadOnlyCollection<Transaction>>(
                                                           new Persistence.GetTransactions(),
                                                           TimeSpan.FromSeconds(5)).Result;

            return Content(JsonConvert.SerializeObject(transactions), "application/json", Encoding.UTF8);
        }

        [HttpGet("transactions/{id}")]
        public IActionResult GetTransaction(string id)
        {
            var transaction = _persistenceActorRef.GetActorRef()
                                                  .Ask<Transaction>(
                                                          new Persistence.GetTransaction(id),
                                                          TimeSpan.FromSeconds(5)).Result;

            return Content(JsonConvert.SerializeObject(transaction), "application/json", Encoding.UTF8);
        }

        [HttpPost("transactions")]
        public IActionResult AddTransaction([FromBody] TransactionDto transactionDto)
        {
            var transaction = new Transaction(Guid.NewGuid(), DateTime.Now.ToUnixTimestamp(), transactionDto.payload);
            _persistenceActorRef.GetActorRef().Tell(transaction);
            return Content(JsonConvert.SerializeObject(transaction), "application/json", Encoding.UTF8);
        }
    }
}

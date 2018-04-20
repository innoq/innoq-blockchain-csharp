namespace Com.Innoq.SharpestChain.Controllers
{
    using System;
    using System.Linq;
    using System.Text;

    using Blocks;

    using Data;

    using IO;

    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    [Route("/")]
    public class SharpestChainController : Controller
    {
        private readonly string _nodeId = Guid.NewGuid().ToString();

        // GET /
        [HttpGet]
        public IActionResult NodeInfo()
        {
            var nodeInfo = new NodeInfo
                           {
                                   NodeId = _nodeId,
                                   CurrentBlockHeight = Persistence.Get().Last().Index
                           };

            return Content(JsonConvert.SerializeObject(nodeInfo), "application/json", Encoding.UTF8);
        }

        // GET blocks
        [HttpGet("blocks")]
        public IActionResult Blocks()
        {
            return Content(JsonConvert.SerializeObject(Persistence.Get()), "application/json", Encoding.UTF8);
        }

        // GET mine
        [HttpGet("mine")]
        public IActionResult Mine()
        {
            var block = Miner.FindNewBlock(Persistence.Get().Last());
            Persistence.Append(block);
            return Content(block.toJson(), "application/json", Encoding.UTF8);
        }
    }
}

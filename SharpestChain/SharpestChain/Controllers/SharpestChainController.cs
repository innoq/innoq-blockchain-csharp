namespace Com.Innoq.SharpestChain.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    using IO;

    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;
    
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    
    using System.Threading.Tasks;

    using Blocks;

    using data;

    [Route("/")]
    public class SharpestChainController : Controller
    {
        private readonly Persistence _persistence;

        private readonly string _nodeId = Guid.NewGuid().ToString();
        public SharpestChainController()
        {
            _persistence = new Persistence();
        }

        // GET /
        [HttpGet]
        public object NodeInfo()
        {
            var nodeInfo = new NodeInfo();
            nodeInfo.NodeId = _nodeId;
            nodeInfo.CurrentBlockHeight = _persistence.Get().Last().Index;
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(nodeInfo));
            var stream = new MemoryStream(byteArray);
            
            return new FileStreamResult(stream, "application/json");
        }

        // GET blocks
        [HttpGet("blocks")]
        public async Task<IActionResult> Blocks()
        {
            
           var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_persistence.Get()));
           var stream = new MemoryStream(byteArray);
            
           return new FileStreamResult(stream, "application/json");
        }

        // GET mine
        [HttpGet("mine")]
        public async Task<IActionResult> Mine()
        {
           var block = ProofFinder.BlockFinder(_persistence.Get().Last());
            
            _persistence.Append(block);
            
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(block));
            var stream = new MemoryStream(byteArray);
            
            return new FileStreamResult(stream, "application/json");
            
            
        }
    }
}

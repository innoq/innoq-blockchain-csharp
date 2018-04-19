namespace Com.Innoq.SharpestChain.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    using IO;

    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;
        
    using System.Threading.Tasks;

    using Blocks;

    using data;

    [Route("/")]
    public class SharpestChainController : Controller
    {
      
        private readonly string _nodeId = Guid.NewGuid().ToString();
        
        // GET /
        [HttpGet]
        public async Task<IActionResult> NodeInfo()
        {
            var nodeInfo = new NodeInfo
                           {
                                   NodeId = _nodeId,
                                   CurrentBlockHeight = Persistence.Get().Last().Index
                           };
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(nodeInfo));
            var stream = new MemoryStream(byteArray);
            
            return await Task.FromResult(new FileStreamResult(stream, "application/json"));
        }

        // GET blocks
        [HttpGet("blocks")]
        public async Task<IActionResult> Blocks()
        {
            
           var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Persistence.Get()));
           var stream = new MemoryStream(byteArray);
            
           return await Task.FromResult(new FileStreamResult(stream, "application/json"));
        }

        // GET mine
        [HttpGet("mine")]
        public async Task<IActionResult> Mine()
        {
           var block = Miner.BlockFinder(Persistence.Get().Last());
            
            Persistence.Append(block);
           
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(block));
            var stream = new MemoryStream(byteArray);
            
            return await Task.FromResult(new FileStreamResult(stream, "application/json"));
            
            
        }
    }
}

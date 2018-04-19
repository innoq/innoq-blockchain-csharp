namespace SharpestChain.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    [Route("/")]
    public class SharpestChainController : Controller
    {
        
        
        // GET /
        [HttpGet]
        //public IEnumerable<string> Get()
        public object NodeInfo()
        {
            Response.ContentType = "application/json";
            return new {nodeId = "bcfeb8c5-c9a6-4731-9a17-e0fedd7aa073", currentBlockHeight = 69};
        }

        // GET blocks
        [HttpGet("blocks")]
        public object Blocks()
        {
            Response.ContentType = "application/json";
            return new { blocks = new []{"dummy"} };
        }

        // GET mine
        [HttpGet("mine")]
        public object Mine()
        {
            Response.ContentType = "application/json";
            return new { mined = "true" };
        }
    }
}

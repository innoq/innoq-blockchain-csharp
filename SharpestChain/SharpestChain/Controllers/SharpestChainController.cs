namespace Com.Innoq.SharpestChain.Controllers
{
    using System.IO;
    using System.Text;

    using IO;

    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;
    
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    
    using System.Threading.Tasks;

    [Route("/")]
    public class SharpestChainController : Controller
    {
        private readonly Persistence _persistence;
        public SharpestChainController()
        {
            _persistence = new Persistence();
        }

        // GET /
        [HttpGet]
        public object NodeInfo()
        {
            Response.ContentType = "application/json";
            return new {nodeId = "bcfeb8c5-c9a6-4731-9a17-e0fedd7aa073", currentBlockHeight = 69};
        }

        // GET blocks
        [HttpGet("blocks")]
        public async Task<IActionResult> Blocks(ActionContext context)
        {
            
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_persistence.Get()));
            var stream = new MemoryStream(byteArray);
            
           return new FileStreamResult(stream, "application/json");
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

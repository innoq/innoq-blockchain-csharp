namespace Com.Innoq.SharpestChain.Data
{
    using Newtonsoft.Json;

    public class NodeInfo
    {
        [JsonProperty("nodeId")]
        public string NodeId { get; set; }

        [JsonProperty("currentBlockHeight")]
        public long CurrentBlockHeight { get; set; }
    }
}

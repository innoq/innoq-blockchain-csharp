namespace Com.Innoq.SharpestChain.Data
{
    using Newtonsoft.Json;

    public static class BlockExtension
    {
        public static string toJson(this Block block)
        {
            return JsonConvert.SerializeObject(block);
        }
    }
}

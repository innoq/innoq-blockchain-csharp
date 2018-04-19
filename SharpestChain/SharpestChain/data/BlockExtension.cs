namespace Com.Innoq.SharpestChain.data
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
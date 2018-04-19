namespace Com.Innoq.SharpestChain.data
{
    using Newtonsoft.Json;

    public static class TransactionExtension
    {
        public static string toJson(this Transaction transaction)
        {
            return JsonConvert.SerializeObject(transaction);
        }
    }
}
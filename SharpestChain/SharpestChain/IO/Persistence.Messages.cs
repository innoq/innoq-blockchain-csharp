namespace Com.Innoq.SharpestChain.IO
{
    public partial class Persistence
    {
        public sealed class GetBlocks
        {
        }

        public sealed class GetTransactions
        {
        }

        public sealed class GetUnconfirmedTransactions
        {
        }

        public sealed class GetTransaction
        {
            public GetTransaction(string id)
            {
                Id = id;
            }

            public string Id;
        }
    }
}

namespace SharpestChain.Data
{
    public class Block
    {
        public Block(long pIndex, long pTimestamp, long pProof, Transaction[] pTransactions, string pPreviousBlockHash)
        {
            Index = pIndex;
            Timestamp = pTimestamp;
            Proof = pProof;
            Transactions = pTransactions;
            PreviousBlockHash = pPreviousBlockHash;
        }

        public long Index { get; }
        public long Timestamp { get; }
        public long Proof { get; }
        public Transaction[] Transactions { get; }
        public string PreviousBlockHash { get; }
    }
}

namespace SharpestChain.data
{
    using System;

    public class Block
    {
        public long Index;
        public long Timestamp;
        public long Proof;
        public Transpaction[] Transactions;
        public string PreviousBlockHash;
    }

    public class Transpaction
    {
        public Guid Id;
        public long Timestamp;
        public string Payload;
    }
}

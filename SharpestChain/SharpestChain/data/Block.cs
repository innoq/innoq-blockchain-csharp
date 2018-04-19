﻿namespace SharpestChain.Data
{
    using Newtonsoft.Json;

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

        [JsonProperty(Order = 1)]
        public long Index { get; }

        [JsonProperty(Order = 2)]
        public string PreviousBlockHash { get; }

        [JsonProperty(Order = 3)]
        public long Proof { get; }

        [JsonProperty(Order = 4)]
        public long Timestamp { get; }

        [JsonProperty(Order = 5)]
        public Transaction[] Transactions { get; }
    }
}
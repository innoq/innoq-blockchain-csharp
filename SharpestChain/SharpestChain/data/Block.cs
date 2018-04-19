﻿namespace Com.Innoq.SharpestChain.data
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

        [JsonProperty(Order = 5)]
        public string PreviousBlockHash { get; }

        [JsonProperty(Order = 3)]
        public long Proof { get; set; }

        [JsonProperty(Order = 2)]
        public long Timestamp { get; }

        [JsonProperty(Order = 4)]
        public Transaction[] Transactions { get; }
        
        public static Block fromJson(string json)
        {
            return JsonConvert.DeserializeObject<Block>(json);
        }
        
        public override bool Equals (object block) 
        {
            var other = block as Block;

            if (other == null) {
                // it is not a Block, so definitely not equal!
                return false;
            }

            return this == other || Equals(other);
        }

        private bool Equals(Block that)
        {
            return Index == that.Index && 
                   string.Equals(PreviousBlockHash, that.PreviousBlockHash) && 
                   Proof == that.Proof && 
                   Timestamp == that.Timestamp && 
                   Transactions.Equals(that.Transactions);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Index.GetHashCode();
                hashCode = (hashCode * 397) ^ PreviousBlockHash.GetHashCode();
                hashCode = (hashCode * 397) ^ Proof.GetHashCode();
                hashCode = (hashCode * 397) ^ Timestamp.GetHashCode();
                hashCode = (hashCode * 397) ^ Transactions.GetHashCode();
                return hashCode;
            }
        }
    }
}

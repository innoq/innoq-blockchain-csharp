namespace Com.Innoq.SharpestChain.Data
{
    using Newtonsoft.Json;

    public class Block
    {
        [JsonProperty(Order = 1)]
        private long index;
        
        [JsonProperty(Order = 5)]
        private string previousBlockHash;
        
        [JsonProperty(Order = 3)]
        private long proof;
        
        [JsonProperty(Order = 2)]
        private long timestamp;

        [JsonProperty(Order = 4)]
        private Transaction[] transactions;

        public Block(long pIndex, long pTimestamp, long pProof, Transaction[] pTransactions, string pPreviousBlockHash)
        {
            Index = pIndex;
            Timestamp = pTimestamp;
            Proof = pProof;
            Transactions = pTransactions;
            PreviousBlockHash = pPreviousBlockHash;
        }

        [JsonIgnore]
        public long Index
        {
            get => index;
            private set => index = value;
        }

        [JsonIgnore]
        public string PreviousBlockHash
        {
            get => previousBlockHash;
            private set => previousBlockHash = value;
        }

        [JsonIgnore]
        public long Proof
        {
            get => proof;
            set => proof = value;
        }


        [JsonIgnore]
        public long Timestamp
        {
            get => timestamp;
            private set => timestamp = value;
        }

        [JsonIgnore]
        public Transaction[] Transactions
        {
            get => transactions;
            private set => transactions = value;
        }

        public static Block FromJson(string json)
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

namespace Com.Innoq.SharpestChain.Data
{
    using Newtonsoft.Json;

    public class Block
    {
        public Block(long pIndex, long pTimestamp, long pProof, Transaction[] pTransactions, string pPreviousBlockHash)
        {
            index = pIndex;
            timestamp = pTimestamp;
            proof = pProof;
            transactions = pTransactions;
            previousBlockHash = pPreviousBlockHash;
        }

        [JsonProperty(Order = 1)]
        public long index { get; set; }

        [JsonProperty(Order = 5)]
        public string previousBlockHash { get; set; }

        [JsonProperty(Order = 3)]
        public long proof { get; set; }

        [JsonProperty(Order = 2)]
        public long timestamp { get; set; }

        [JsonProperty(Order = 4)]
        public Transaction[] transactions { get; set; }
        
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
            return index == that.index && 
                   string.Equals(previousBlockHash, that.previousBlockHash) && 
                   proof == that.proof && 
                   timestamp == that.timestamp && 
                   transactions.Equals(that.transactions);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = index.GetHashCode();
                hashCode = (hashCode * 397) ^ previousBlockHash.GetHashCode();
                hashCode = (hashCode * 397) ^ proof.GetHashCode();
                hashCode = (hashCode * 397) ^ timestamp.GetHashCode();
                hashCode = (hashCode * 397) ^ transactions.GetHashCode();
                return hashCode;
            }
        }
    }
}

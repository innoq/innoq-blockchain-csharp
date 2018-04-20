namespace Com.Innoq.SharpestChain.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public class Block
    {
        public Block(long pIndex, long pTimestamp, long pProof, IEnumerable<Transaction> pTransactions,
                     string pPreviousBlockHash)
        {
            Index = pIndex;
            Timestamp = pTimestamp;
            Proof = pProof;
            if (pTransactions != null && pTransactions.Any())
            {
                Transactions.AddRange(pTransactions);
            }

            PreviousBlockHash = pPreviousBlockHash;
        }

        [JsonProperty("index", Order = 1)]
        public long Index { get; set; }

        [JsonProperty("previousBlockHash", Order = 5)]
        public string PreviousBlockHash { get; set; }

        [JsonProperty("proof", Order = 3)]
        public long Proof { get; set; }

        [JsonProperty("timestamp", Order = 2)]
        public long Timestamp { get; set; }

        [JsonProperty("transactions", Order = 4)]
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public static Block FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Block>(json);
        }

        public void IncrementProof()
        {
            Proof += 1;
        }

        public override bool Equals(object block)
        {
            var other = block as Block;

            if (other == null)
            {
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

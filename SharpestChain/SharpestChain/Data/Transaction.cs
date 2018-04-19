namespace Com.Innoq.SharpestChain.Data
{
    using System;

    using Newtonsoft.Json;

    public class Transaction
    {
        [JsonProperty(Order = 1)]
        private Guid id;

        [JsonProperty(Order = 3)]
        private string payload;

        [JsonProperty(Order = 2)]
        private long timestamp;

        public Transaction(Guid pId, long pTimestamp, string pPayload)
        {
            Id = pId;
            Timestamp = pTimestamp;
            Payload = pPayload;
        }

        public Guid Id
        {
            get => id;
            private set => id = value;
        }

        public string Payload
        {
            get => payload;
            private set => payload = value;
        }

        public long Timestamp
        {
            get => timestamp;
            private set => timestamp = value;
        }

        public static Transaction fromJson(string json)
        {
            return JsonConvert.DeserializeObject<Transaction>(json);
        }
    }
}

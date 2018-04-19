namespace Com.Innoq.SharpestChain.Data
{
    using System;

    using Newtonsoft.Json;

    public class Transaction
    {
        public Transaction(Guid pId, long pTimestamp, string pPayload)
        {
            id = pId;
            timestamp = pTimestamp;
            payload = pPayload;
        }

        [JsonProperty(Order = 1)]
        public Guid id { get; set; }

        [JsonProperty(Order = 3)]
        public string payload { get; set; }

        [JsonProperty(Order = 2)]
        public long timestamp { get; set; }

        public static Transaction fromJson(string json)
        {
            return JsonConvert.DeserializeObject<Transaction>(json);
        }
    }
}

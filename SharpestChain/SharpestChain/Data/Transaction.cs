﻿namespace Com.Innoq.SharpestChain.Data
{
    using System;

    using Newtonsoft.Json;

    public class Transaction
    {
        public Transaction(Guid pId, long pTimestamp, string pPayload)
        {
            Id = pId;
            Timestamp = pTimestamp;
            Payload = pPayload;
        }

        [JsonProperty("id", Order = 1)]
        public Guid Id { get; private set; }

        [JsonProperty("payload", Order = 3)]
        public string Payload { get; private set; }

        [JsonProperty("timestamp", Order = 2)]
        public long Timestamp { get; private set; }

        public static Transaction fromJson(string json)
        {
            return JsonConvert.DeserializeObject<Transaction>(json);
        }
    }
}

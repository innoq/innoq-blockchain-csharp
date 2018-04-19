﻿namespace SharpestChain.Data
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

        [JsonProperty(Order = 1)]
        public Guid Id { get; }

        [JsonProperty(Order = 2)]
        public string Payload { get; }

        [JsonProperty(Order = 3)]
        public long Timestamp { get; }
    }
}
namespace SharpestChain.Data
{
    using System;

    public class Transaction
    {
        public Transaction(Guid pId, long pTimestamp, string pPayload)
        {
            Id = pId;
            Timestamp = pTimestamp;
            Payload = pPayload;
        }

        public Guid Id { get; }
        public long Timestamp { get; }
        public string Payload { get; }
    }
}
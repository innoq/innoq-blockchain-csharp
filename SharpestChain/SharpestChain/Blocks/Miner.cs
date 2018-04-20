namespace Com.Innoq.SharpestChain.Blocks
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Akka.Actor;

    using Cryptography;

    using Data;

    using IO;

    using Util;

    public static class Miner
    {
        public static Block FindNewBlock(IPersistenceActorRef persistenceActorRef)
        {
            var blocks = persistenceActorRef.GetActorRef()
                                            .Ask<ReadOnlyCollection<Block>>(
                                                    new Persistence.GetBlocks(),
                                                    TimeSpan.FromSeconds(5)).Result;
            var previousBlock = blocks.Last();
            string hash = SHA256Encoder.EncodeString(previousBlock.toJson());

            var transactions = persistenceActorRef.GetActorRef()
                                                  .Ask<ReadOnlyCollection<Transaction>>(
                                                          new Persistence.GetUnconfirmedTransactions(),
                                                          TimeSpan.FromSeconds(5)).Result
                                                  .Take(5);
            var candidate = new Block(previousBlock.Index + 1, DateTime.Now.ToUnixTimestamp(), 0, transactions, hash);


            string candidateHash = SHA256Encoder.EncodeString(candidate.toJson());


            while (!candidateHash.StartsWith("0000", StringComparison.Ordinal))
            {
                candidate.IncrementProof();
                candidateHash = SHA256Encoder.EncodeString(candidate.toJson());
            }

            return candidate;
        }
    }
}

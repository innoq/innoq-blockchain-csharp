namespace Com.Innoq.SharpestChain.Blocks
{
    using System;

    using Data;

    using Cryptography;

    using Util;

    public static class Miner
    {
        public static Block FindNewBlock(Block prevblock)
        {
            string hash = SHA256Encoder.EncodeString(prevblock.toJson());
            var candidate = new Block(prevblock.Index + 1, DateTime.Now.ToUnixTimestamp(), 0, new Transaction[] { },
                                      hash);

            while (true)
            {
                string candidateHash = SHA256Encoder.EncodeString(candidate.toJson());

                if (candidateHash.StartsWith("0000", StringComparison.Ordinal))
                {
                    break;
                }

                candidate.IncrementProof();
            }

            return candidate;
        }
    }
}

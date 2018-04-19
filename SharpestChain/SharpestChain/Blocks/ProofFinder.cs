namespace Com.Innoq.SharpestChain.Blocks
{
    using System;

    using Cryptography;

    using data;

    using Util;

    public class ProofFinder
    {
       
        public ProofFinder()
        {
        }

        public static Block BlockFinder(Block prevblock)
        {
            string hash = SHA256Encoder.EncodeString(prevblock.toJson());
            var candidate = new Block(prevblock.Index +1, new DateTime().ToUnixTimestamp(), 0, new Transaction[]{},hash);
            while (true)
            {
                string candidateHash = SHA256Encoder.EncodeString(candidate.toJson());

                if (candidateHash.StartsWith("0000"))
                {
                    break;   
                }
               
                
                candidate.Proof = candidate.Proof + 1;
                
               
            }
            
            return candidate;
        }

    }
}

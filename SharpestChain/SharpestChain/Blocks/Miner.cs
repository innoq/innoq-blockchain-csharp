﻿namespace Com.Innoq.SharpestChain.Blocks
{
    using System;

    using Data;

    using Util;

    internal static class Miner
    {

        internal static Block MineNewBlock(Block previousBlock)
        {

            // hash previous Block

            string previousBlockHash = string.Empty; // SHA256Encoder.EncodeString)

            var newBlock = new Block(previousBlock.index + 1, DateTime.Now.ToUnixTimestamp(), 0, null, previousBlockHash);

            // try to find new block


            // get unix ts
            // int proof = ProofFinder.FindNewProof(previousBlockHash, newBlock);



            // return object



            return null;
        }


    }

}

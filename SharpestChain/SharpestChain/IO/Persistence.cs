namespace Com.Innoq.SharpestChain.IO
{
    using System;
    using System.Collections.Generic;

    using Data;

    public static class Persistence
    {
        public static readonly Block GENESIS_BLOCK = 
                new Block(1, 0, 1917336, new []{new Transaction(new Guid("b3c973e2-db05-4eb5-9668-3e81c7389a6d"), 0, "I am Heribert Innoq")}, "0");
        
        private static readonly List<Block> _blocks = new List<Block>{GENESIS_BLOCK};

        public static List<Block> Get()
        {
            lock (_blocks)
            {
                return new List<Block>(_blocks);    
            }
            
        }

        public static void Append(Block block)
        {

            lock (_blocks)
            {
                _blocks.Add(block);    
            }
            
        }
    }
}

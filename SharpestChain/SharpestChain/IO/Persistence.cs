namespace Com.Innoq.SharpestChain.IO
{
    using System;
    using System.Linq;

    using data;

    public class Persistence
    {
        public static readonly Block GENESIS_BLOCK = 
                new Block(1, 0, 1917336, new []{new Transaction(new Guid("b3c973e2-db05-4eb5-9668-3e81c7389a6d"), 0, "I am Heribert Innoq")}, "0");
        
        private Block[] _blocks = {GENESIS_BLOCK};

        public Block[] Get()
        {
            return _blocks;
        }

        public void Append(Block block)
        {
            _blocks = _blocks.Concat(new []{block}).ToArray();
        }
    }
}

namespace SharpestChain.IO
{
    using Data;

    public class Persistence
    {
        private static Block[] blocks;
        
        public static Block[] Load()
        {
            return blocks;
        }

        public static void Save(Block[] blocks)
        {
            Persistence.blocks = blocks;
        }
    }
}

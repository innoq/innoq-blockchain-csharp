namespace Com.Innoq.SharpestChain.IO
{
    using data;

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

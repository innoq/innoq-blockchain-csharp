using NUnit.Framework;

namespace SharpestChainTest
{
    using SharpestChain.Data;
    using SharpestChain.IO;

    [TestFixture]
    public class PersistenceTest
    {
        [Test]
        public void LoadWhatWasSaved()
        {
            var block = new Block(1, 0, 0, new Transaction[0], "");
            var loaded = Persistence.Load();

            Assert.Equals(loaded[0], block);
        }
    }
}

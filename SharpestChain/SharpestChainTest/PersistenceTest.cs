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
            Persistence.Save(new[]{block});
            var loaded = Persistence.Load();

            Assert.That(loaded[0], Is.EqualTo(block));
        }
    }
}

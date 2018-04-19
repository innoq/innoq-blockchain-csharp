using NUnit.Framework;

namespace Com.Innoq.SharpestChain
{
    using System.Collections.Generic;

    using Data;

    using IO;

    [TestFixture]
    public class PersistenceTest
    {
        [Test]
        public void Get_ReturnsGenesisBlock_RightAfterInitialization()
        {
            var loaded = Persistence.Get();
            Assert.That(loaded[0], Is.EqualTo(Persistence.GENESIS_BLOCK));
        }

        [Test]
        public void Append_AppendsBlockAtTheEnd()
        {
            var oldChain = Persistence.Get();
            var block = new Block(3, 0, 0, new Transaction[0], "");
            var newChain = new List<Block>(oldChain) {block};
            Persistence.Append(block);
            
            var loaded = Persistence.Get();
            Assert.That(loaded, Is.EquivalentTo(newChain));
        }
    }
}

﻿using NUnit.Framework;

namespace Com.Innoq.SharpestChain
{
    using System.Linq;

    using data;

    using IO;

    [TestFixture]
    public class PersistenceTest
    {
        [Test]
        public void Get_ReturnsGenesisBlock_RightAfterInitialization()
        {
            var persistence = new Persistence();
            var loaded = persistence.Get();
            Assert.That(loaded[0], Is.EqualTo(Persistence.GENESIS_BLOCK));
        }

        [Test]
        public void Append_AppendsBlockAtTheEnd()
        {
            var persistence = new Persistence();
            var old = persistence.Get();
            var block = new Block(3, 0, 0, new Transaction[0], "");
            persistence.Append(block);
            
            var loaded = persistence.Get();
            Assert.That(loaded, Is.EqualTo(old.Concat(new []{block}).ToArray()));
        }
    }
}

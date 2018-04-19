namespace Com.Innoq.SharpestChain.Data
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class BlockTest
    {
        [Test]
        public void deserializeSerialize()
        {
            var jsonString = "{\"index\": 1," +
                             "\"timestamp\": 0," +
                             "\"proof\": 955977," +
                             "\"transactions\": [{" +
                             "\"id\": \"b3c973e2-db05-4eb5-9668-3e81c7389a6d\"," +
                             "\"timestamp\": 0," +
                             "\"payload\": \"I am Heribert Innoq\"}]," +
                             "\"previousBlockHash\": \"0\"}";

            var block = Block.fromJson(jsonString);
            var transaction = new Transaction(new Guid("b3c973e2-db05-4eb5-9668-3e81c7389a6d"), 0, "I am Heribert Innoq");

            Assert.That(block.index, Is.EqualTo(1));
            Assert.That(block.previousBlockHash, Is.EqualTo("0"));
            Assert.That(block.timestamp, Is.EqualTo(0));
            Assert.That(block.proof, Is.EqualTo(955977));
            
            Assert.That(block.transactions.Length, Is.EqualTo(1));
            Assert.That(block.transactions[0].toJson(), Is.EqualTo(transaction.toJson()));
        }

        [Test]
        public void serializeDeserialize()
        {
            var transaction =
                    new Transaction(new Guid("b3c973e2-db05-4eb5-9668-3e81c7389a6d"), 0, "I am Heribert Innoq");
            var block = new Block(1, 0, 955977, new []{transaction}, "0");

            string jsonString = block.toJson();
            var fromJson = Block.fromJson(jsonString);

            Assert.That(fromJson.toJson(), Is.EqualTo(block.toJson()),
                        "Original: " + jsonString + "\n" + "Converted: " + fromJson.toJson());
        }
    }
}

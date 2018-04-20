namespace Com.Innoq.SharpestChain.Data
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class TransactionTest
    {
        [Test]
        public void createFromJson()
        {
            string jsonString = "{" +
                                "\"id\": \"b3c973e2-db05-4eb5-9668-3e81c7389a6d\"," +
                                "\"timestamp\": 0," +
                                "\"payload\": \"I am Heribert Innoq\"}";

            var transaction = Transaction.fromJson(jsonString);

            Assert.That(transaction.Id, Is.EqualTo(new Guid("b3c973e2-db05-4eb5-9668-3e81c7389a6d")));
            Assert.That(transaction.Timestamp, Is.EqualTo(0));
            Assert.That(transaction.Payload, Is.EqualTo("I am Heribert Innoq"));
        }

        [Test]
        public void serializeDeserialize()
        {
            var transaction =
                    new Transaction(new Guid("b3c973e2-db05-4eb5-9668-3e81c7389a6d"), 0, "I am Heribert Innoq");

            string jsonString = transaction.toJson();
            var fromJson = Transaction.fromJson(jsonString);

            Assert.That(fromJson.toJson(), Is.EqualTo(transaction.toJson()),
                        "Original: " + jsonString + "\n" + "Converted: " + fromJson.toJson());
        }
    }
}

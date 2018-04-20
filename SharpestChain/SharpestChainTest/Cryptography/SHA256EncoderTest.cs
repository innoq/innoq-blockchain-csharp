namespace Com.Innoq.SharpestChain.Cryptography
{
    using NUnit.Framework;

    [TestFixture]
    public class SHA256EncoderTest
    {
        [TestCase(null, "")]
        [TestCase("", "")]
        [TestCase("I am Peter Pan", "10a09abd7f9234503a9941139dcdbd1bc4b1705beb5c3702d4b08142beb0ac6a")]
        public void TestEncoding(string originalString, string hash)
        {
            Assert.That(hash, Is.EqualTo(SHA256Encoder.EncodeString(originalString)));
        }
    }
}

namespace SharpestChain.Cryptography
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Class provides methods to encode any given string to a sha256 encoded string.
    /// </summary>
    internal static class SHA256Encoder
    {

        private static readonly SHA256 encoder = SHA256.Create();

        /// <summary>
        /// Method encodes a given string to an SHA256 encoded string. If <paramref name="givenString"/> is <c>null</c> or empty,
        /// an empty string is returned. For conversion, the ASCII bytes of the string are used. The resulting bytearry is again converted to
        /// a string, based on ASCII encoding.
        /// </summary>
        /// <param name="givenString">String that will be converted.</param>
        /// <returns>SHA256 encoded version of <paramref name="givenString"/></returns>
        internal static string EncodeString(string givenString)
        {
            return string.IsNullOrEmpty(givenString)
                    ? string.Empty
                    : Encoding.ASCII.GetString(encoder.ComputeHash(Encoding.ASCII.GetBytes(givenString)));
        }
    }
}

namespace Com.Innoq.SharpestChain.Util
{
    using System;

    public static class DateTimeExtension
    {
        private static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1);

        public static long ToUnixTimestamp(this DateTime timestamp) => (long) timestamp.Subtract(UNIX_EPOCH).TotalSeconds;

    }
}

using System;

namespace IBUtility
{
    /// <summary>
    /// Converts TimeStamp to DateTime and vice versa
    /// </summary>
    public static class TimeStampConverter
    {
        public static DateTime ToDateTime(Int64 timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
        public static DateTime ToDateTime(string timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(System.Convert.ToInt64(timestamp));
        }

        /// <summary>
        /// Converts a given DateTime into a Unix timestamp
        /// </summary>
        /// <param name="value">Any DateTime</param>
        /// <returns>The given DateTime in Unix timestamp format</returns>
        public static long ToUnixTimestamp(DateTime value)
        {
            return (long)Math.Truncate((value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }
    }
}

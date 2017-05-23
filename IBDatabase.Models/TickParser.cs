using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBDatabase.Models
{
    public static class TickParser
    {
        public static int GetTickerId(string[] split)
        {
            return (from f in split
                where f.ToLower().Contains("ticker")
                select f.Split(':')[1].Trim() into temp
                select Int32.Parse(temp)).FirstOrDefault();
        }

        public static Double GetOptionField(string[] split, string identifier)
        {
            return (from f in split
                where f.ToLower().Contains(identifier)
                select f.Split(':')[1].Trim() into temp
                select Double.Parse(temp)).FirstOrDefault();
        }

        public static int GetTickType(string[] split)
        {
            return (from f in split
                where f.ToLower().Contains("field")
                select f.Split(':')[1].Trim() into temp
                select Int32.Parse(temp)).FirstOrDefault();
        }

        public static int GetTickValue(string[] split)
        {
            return (from f in split
                where f.ToLower().Contains("value:")
                select f.Split(':')[1].Trim() into temp
                select Int32.Parse(temp)).FirstOrDefault();
        }

        public static int GetTickSize(string[] split)
        {
            return (from f in split
                where f.ToLower().Contains("size:")
                select f.Split(':')[1].Trim() into temp
                select Int32.Parse(temp)).FirstOrDefault();
        }

        public static decimal GetTickPrice(string[] split)
        {
            return (from f in split
                where f.ToLower().Contains("price:")
                select f.Split(':')[1].Trim() into temp
                select ToDecimal(temp)).FirstOrDefault<decimal>();
        }

        public static int GetTickCanAutoExecute(string[] split)
        {
            return (from f in split
                where f.ToLower().Contains("execute")
                select f.Split(':')[1].Trim() into temp
                select Int32.Parse(temp)).FirstOrDefault();
        }

        public static int ToInt(string data)
        {
            return Int32.Parse(data.Substring(data.IndexOf(":", StringComparison.Ordinal) + 1).Trim());
        }

        public static decimal ToDecimal(string data)
        {
            return Decimal.Parse(data.Substring(data.IndexOf(":", StringComparison.Ordinal) + 1).Trim());
        }

        public static decimal ToDecimal(double data)
        {
            return System.Convert.ToDecimal(data);
        }

        /// <summary>
        /// Converts the long timestamp from a unix timestamp to a C# DateTime
        /// </summary>
        /// <param name="timestamp">the unix timestamp</param>
        /// <returns>The timestamp as a DateTime</returns>
        /// <see cref="EWrapperImpl.ConvertFromUnixTimestamp"/>
        public static DateTime ToDateTime(long timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}

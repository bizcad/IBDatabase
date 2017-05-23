using System.Collections.Generic;
using IBDatabase.Models;

namespace IBDatabase.Redis
{
    public interface ISqlRedis<T>
    {
        int RefreshRedisFromDb();

        /// <summary>
        /// Gets items in the database expiring tomorrow and later
        /// </summary>
        /// <returns>the list</returns>
        List<T> GetListFromSqlLaterThanToday();

        /// <summary>
        /// Gets all the items in the database; not just those that expire later than today
        /// </summary>
        /// <returns>the list</returns>
        List<T> GetListFromSqlAll();

        /// <summary>
        /// Upcerts entries int the database from Redis
        /// </summary>
        /// <returns>the number of keys in redis</returns>
        int RefreshDbFromRedis();

        /// <summary>
        /// Gets a Dictionary from Redis
        /// </summary>
        /// <returns>The Dictionary</returns>
        Dictionary<string, T> GetDictionaryFromRedis();

        /// <summary>
        /// Gets a Dictionary From the database
        /// </summary>
        /// <returns>The Dictionary</returns>
        Dictionary<int, T> GetDictionaryFromSql();

        IEnumerable<string> GetKeys(string search);
        void Dispose();
    }
}
using System.Collections.Generic;
//using IBDatabase.Migrations;
using IBDatabase.Models;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;

namespace IBDatabase.Redis
{
    public interface IGetter<T>
    {
        T Set(T contract);
        T Get(long conId);
        Dictionary<string, T> GetDictionary();
        List<T> GetList();
        Dictionary<string, T> SaveJson(string whereSerialized);
        List<T> SaveCsv(string whereSerialized);
        IDatabase Db { get; set; }
        StackExchangeRedisCacheClient Client { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;
using IBDatabase.Models;

namespace IBDatabase.Redis
{
    public class ContractSqlRedis : IDisposable, ISqlRedis<DBContract>
    {
        private readonly StackExchangeRedisCacheClient _cacheClient;
        private const string RedisKey = "contract:";
        private const string JsonFilename = "contracts.json";

        public ContractSqlRedis()
        {
            NewtonsoftSerializer serializer = new NewtonsoftSerializer();
            _cacheClient = new StackExchangeRedisCacheClient(serializer);
        }

        public int RefreshRedisFromDb()
        {
            int count = 0;
            //var contracts = GetListFromSqlLaterThanToday();
            //var c = repo.Get().Where(r => r.ConId == 257272500);
            //var d = _contracts.Where(r => r.ConId == 257272500);
            //IO.SerializeJson(_contracts, JsonFilename);
            FlushRedis();

            foreach (var contract in GetListFromSqlLaterThanToday())
            {
                _cacheClient.Add($"{RedisKey}" + contract.ConId, contract);
                count++;
            }
            return count;
        }

        private void FlushRedis()
        {
            foreach (string key in GetKeys($"{RedisKey}*"))
            {
                _cacheClient.Remove(key);
            }
        }
        /// <summary>
        /// Gets items in the database expiring tomorrow and later
        /// </summary>
        /// <returns>the list</returns>
        public List<DBContract> GetListFromSqlLaterThanToday()
        {
            var repo = new DBContractsConnectedRepository();
            return repo.GetListLaterThanToday();
        }
        /// <summary>
        /// Gets all the items in the database; not just those that expire later than today
        /// </summary>
        /// <returns>the list</returns>
        public List<DBContract> GetListFromSqlAll()
        {
            DBContractsConnectedRepository repo = new DBContractsConnectedRepository();
            List<DBContract> contracts = repo.Get().ToList();
            contracts.AddRange(repo.Get().Where(n => n.Expiry == null));
            return contracts;
        }
        /// <summary>
        /// Upcerts entries int the database from Redis
        /// </summary>
        /// <returns>the number of keys in redis</returns>
        public int RefreshDbFromRedis()
        {
            int count = 0;
            var repo = new DBContractsConnectedRepository();
            foreach (string key in GetKeys($"{RedisKey}*"))
            {
                var contract =_cacheClient.Get<DBContract>(key);
                contract.EntityType = "DBContract";
                repo.InsertUpdate(contract);
                count++;
            }
            return count;
        }
        /// <summary>
        /// Makes a string in DBContract.Expiry format starting with tomorrow
        /// </summary>
        /// <returns>The date as yyyymmdd starting tomorrow</returns>
        private string LaterThanToday()
        {
            DateTime now = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            sb.Append(now.Year);
            sb.Append(now.Month);
            sb.Append(now.Day + 1);
            return sb.ToString();
        }
        /// <summary>
        /// Gets a Dictionary from Redis
        /// </summary>
        /// <returns>The Dictionary</returns>
        public Dictionary<string, DBContract> GetDictionaryFromRedis()
        {
            Dictionary<string, DBContract> dic = new Dictionary<string, DBContract>();
            foreach (string key in GetKeys($"{RedisKey}*"))
            {
                DBContract dBContract = _cacheClient.Get<DBContract>(key);
                dic.Add(key, dBContract);
            }
            return dic;
        }
        /// <summary>
        /// Gets a Dictionary From the database
        /// </summary>
        /// <returns>The Dictionary</returns>
        public Dictionary<int, DBContract> GetDictionaryFromSql()
        {
            Dictionary<int, DBContract> dic = new Dictionary<int, DBContract>();
            foreach (var dbContract in GetListFromSqlLaterThanToday())
            {
                dic.Add(dbContract.ConId, dbContract);
            }
            return dic;
        }


        public IEnumerable<string> GetKeys(string search)
        {
            return _cacheClient.SearchKeys(search);
        }


        public void Dispose()
        {
            _cacheClient.Dispose();
        }
    }
}

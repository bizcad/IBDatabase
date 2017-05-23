using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBDatabase.Factory;
using IBDatabase.Models;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace IBDatabase.Redis
{
    public class QuoteSqlRedis: ISqlRedis<ContractQuote>, IDisposable
    {
        
        private readonly StackExchangeRedisCacheClient _cacheClient;
        private const string RedisKey = "contractquote:";

        public QuoteSqlRedis()
        {
            var serializer = new NewtonsoftSerializer();
            _cacheClient = new StackExchangeRedisCacheClient(serializer);
        }


        public int RefreshRedisFromDb()
        {
            int count = 0;
            //var contracts = GetListFromSqlLaterThanToday();
            //var c = repo.Get().Where(r => r.ConId == 257272500);
            //var d = _contracts.Where(r => r.ConId == 257272500);
            //IO.SerializeJson(_contracts, JsonFilename);
            //FlushRedis();

            foreach (var contractQuote in GetListFromSqlLaterThanToday())
            {
                _cacheClient.Add($"{RedisKey}" + contractQuote.ConId, contractQuote);
                count++;
                //System.Diagnostics.Debug.WriteLine($"{contractQuote.ConId}");
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

        public List<ContractQuote> GetListFromSqlLaterThanToday()
        {
            var crepo = new DBContractsConnectedRepository();
            List<DBContract> contracts =
                crepo.Get().Where(n => String.Compare(n.Expiry, LaterThanToday(), StringComparison.Ordinal) > 0).ToList();
            contracts.AddRange(crepo.Get().Where(n => n.Expiry == null));

            var repo = new ContractQuoteConnectedRepository();
            List<ContractQuote> contractQuotes = new List<ContractQuote>();
            foreach (var contract in contracts)
            {
                var quote = repo.GetByConId(contract.ConId);
                if (quote == null)
                {
                    quote = ContractQuoteFactory.Create(contract);
                    repo.Add(quote);
                }
                contractQuotes.Add(quote);
            }
                
            return contractQuotes;
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

        public List<ContractQuote> GetListFromSqlAll()
        {
            var repo = new ContractQuoteConnectedRepository();
            return repo.Get().ToList();
        }

        public int RefreshDbFromRedis()
        {
            int count = 0;
            var repo = new ContractQuoteConnectedRepository();
            foreach (string key in GetKeys($"{RedisKey}*"))
            {
                var contractQuote = _cacheClient.Get<ContractQuote>(key);
                repo.InsertUpdate(contractQuote);
                count++;
            }
            return count;
        }

        public Dictionary<string, ContractQuote> GetDictionaryFromRedis()
        {
            var dic = new Dictionary<string, ContractQuote>();
            foreach (string key in GetKeys($"{RedisKey}*"))
            {
                var quote = _cacheClient.Get<ContractQuote>(key);
                dic.Add(key, quote);
            }
            return dic;
        }

        public Dictionary<int, ContractQuote> GetDictionaryFromSql()
        {
            var dic = new Dictionary<int, ContractQuote>();
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

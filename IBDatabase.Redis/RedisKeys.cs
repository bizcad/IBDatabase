using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace IBDatabase.Redis
{
    
    public static class RedisKeys
    {
        private const string Connectionstring = "localhost";

        public static List<string> GetKeys()
        {
            var list = new List<string>();
            ContractSqlRedis x = new ContractSqlRedis();
            return x.GetKeys("*").ToList();
        }
        public static List<string> GetContactKeys()
        {
            var list = new List<string>();
            var x = new ContractGetter(new DBContractsConnectedRepository());
            return x.GetKeys().ToList();
        }
        public static List<string> GetContactQuoteKeys()
        {
            var list = new List<string>();
            var quoteGetter = new QuoteGetter(new ContractQuoteConnectedRepository(), new DBContractsConnectedRepository());
            return quoteGetter.GetKeys().ToList();
        }

    }
}

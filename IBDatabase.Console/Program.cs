using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IBDatabase.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace IBDatabase.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize Redis from the database with contracts that expire later than today
            //  By adding the contracts to Redis, the response time speeds up.
            ContractSqlRedis csr = new ContractSqlRedis();
            var t = csr.RefreshRedisFromDb();

            // Create a quote receiver which blocks
            var quoteReciever = new QuoteReceiver();
            quoteReciever.Subscribe();
            var contractReceiver = new ContractReceiver();
            contractReceiver.Subscribe();

            System.Console.WriteLine("press any key...");
            System.Console.ReadKey();
        }

        
    }
}

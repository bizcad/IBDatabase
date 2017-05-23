using System;
using System.Collections.Generic;
using IBDatabase.Factory;
using IBDatabase.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace IBDatabase.Redis
{
    public class ContractReceiver : IReciever<DBContract>
    {
        private const string Connectionstring = "localhost";
        //private readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(Connectionstring);

        //private StackExchangeRedisCacheClient _cacheClient;
        private ContractGetter _getter;
        private DBContractsConnectedRepository _repo;
        public IDatabase Db { get; set; }
        public ISubscriber Sub { get; set; }

        public ContractReceiver()
        {
            //var serializer = new NewtonsoftSerializer();
            //_cacheClient = new StackExchangeRedisCacheClient(serializer);
            _repo = new DBContractsConnectedRepository();
            _getter = new ContractGetter(_repo);
        }

        public void Subscribe()
        {
            Sub = RedisConnectorHelper.Connection.GetSubscriber();
            Sub.Subscribe("contract", (channel, message) =>
            {
                if (Sub.IsConnected("contract"))
                {
                    UpdateRedis(message);
                }
            });

        }

        public void UpdateRedis(string message)
        {
            //Console.WriteLine(message);
            
            string rediskey = "contract:";
            KeyValuePair<int, object> kvp;
            try
            {
                kvp = JsonConvert.DeserializeObject<KeyValuePair<int, object>>(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            dynamic y = kvp.Value;
            rediskey += y.ConId.ToString();
            var db = DBContractFactory.Create(y);
            _getter.Set(db);

            _repo.Add(db);
            //Console.WriteLine(db.Id);
        }

        public void Unsubscribe()
        {
            Sub.Unsubscribe("contract");
        }
    }
}

using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace IBDatabase.Redis
{
    public class GetterBase
    {
        private const string Connectionstring = "localhost";
        public IDatabase Db { get; set; }
        public int Counter;
        public StackExchangeRedisCacheClient Client { get; set; }

        public GetterBase()
        {

            //Redis = ConnectionMultiplexer.Connect(Connectionstring);
            Init();
        }
        //public GetterBase(string connectionString)
        //{
        //    Redis = ConnectionMultiplexer.Connect(connectionString);
        //    Init();
        //}
        //public GetterBase(ConnectionMultiplexer redis)
        //{
        //    this.Redis = redis;
        //    Init();
        //}

        private void Init()
        {
            var serializer = new NewtonsoftSerializer();
            Client = new StackExchangeRedisCacheClient(serializer);
            Db = Client.Database;
        }
    }
}

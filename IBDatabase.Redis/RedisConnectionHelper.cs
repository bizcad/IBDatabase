using StackExchange.Redis;
using System;

namespace IBDatabase.Redis
{
    public static class RedisConnectorHelper
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;
        static RedisConnectorHelper()
        {
            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("localhost"));
        }
        public static ConnectionMultiplexer Connection => LazyConnection.Value;
    }
}

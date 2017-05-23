using System;
using IBDatabase.Redis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IBDatabase.Tests
{
    [TestClass]
    public class IBDatabaseRedisTests
    {
        [TestMethod]
        public void ClearsTheCache()
        {
            var con = IBDatabase.Redis.RedisConnectorHelper.Connection;
            var config = con.Configuration;
           
            
            var db = IBDatabase.Redis.RedisConnectorHelper.Connection.GetServer("localhost",6379);
            db.FlushDatabase();
        }
    }
}

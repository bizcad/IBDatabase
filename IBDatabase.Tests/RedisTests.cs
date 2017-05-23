using System;
using IBDatabase.Redis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IBDatabase.Tests
{
    [TestClass]
    public class RedisTests
    {
        [TestMethod]
        public void GetsAKey()
        {
            QuoteGetter getter = new QuoteGetter();
            var result = getter.Get(416904);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ConId == 416904);

        }

        [TestMethod]
        public void ClearsRedis()
        {
            QuoteGetter geter = new QuoteGetter();
            geter.Client.FlushDb();
        }
        [TestMethod]
        public void SavesRedis()
        {
            var serializer = new NewtonsoftSerializer();
            var cacheClient = new StackExchangeRedisCacheClient(serializer);
            try
            {
                cacheClient.Save(SaveType.BackgroundSave);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

using System;
using IBDatabase.Redis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IBDatabase.Tests
{
    [TestClass]
    public class ContractDBRedisTests
    {
        [TestMethod]
        public void GetsLaterThanToday()
        {
            ContractSqlRedis redis = new ContractSqlRedis();
            var result = redis.GetListFromSqlLaterThanToday();
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void RefreshsContractsToRedis()
        {
            ContractSqlRedis x = new ContractSqlRedis();
            var t = x.RefreshRedisFromDb();
            Assert.IsTrue(t > 0);
        }
        [TestMethod]
        public void RefreshsDbFromRedis()
        {
            ContractSqlRedis x = new ContractSqlRedis();
            var t = x.RefreshDbFromRedis();
            Assert.IsTrue(t > 0);
        }


    }
}

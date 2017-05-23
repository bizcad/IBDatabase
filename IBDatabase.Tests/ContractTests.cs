using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IBDatabase.Models;
using IBDatabase.Redis;
using IBUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace IBDatabase.Tests
{
    [TestClass]
    public class ContractTests
    {
        //[TestMethod]
        //public void GetsDbContractDictionaryJson()
        //{
        //    var result = IO.GetDBContractDictionaryJson("ContractsDictionary.json");
        //    Assert.IsTrue(result.Count > 0);
        //}
        private const string Connectionstring = "localhost";
        private ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(Connectionstring);
        private DBContractsConnectedRepository _crepo;
        [TestInitialize]
        public void Initialize()
        {
            _redis = ConnectionMultiplexer.Connect(Connectionstring);
            _crepo = new DBContractsConnectedRepository();
        }

        [TestMethod]
        public void GetsClientInfo()
        {
            var serializer = new NewtonsoftSerializer();
            StackExchangeRedisCacheClient cacheClient = new StackExchangeRedisCacheClient(serializer);
            var db = cacheClient.Database;
            var info = cacheClient.GetInfo();
            foreach (var i in info)
            {
                Console.WriteLine($"{i.Key} - {i.Value}");
            }
            Assert.IsTrue(info.Count > 0);

        }
        [TestMethod]
        public void GetsKeys()
        {
            var list = new ContractGetter(_crepo).GetKeys().ToList();
            System.Diagnostics.Debug.WriteLine(list.Count);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void GetsContractDictionary()
        {
            var factory = new ContractGetter(_crepo);
            var list = factory.GetDictionary();
            System.Diagnostics.Debug.WriteLine(list.Count);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void SavesCsv()
        {
            var gtr = new ContractGetter(_crepo);
            var result = gtr.SaveCsv("Contract.csv");
            Assert.IsTrue(result.Count > 0);
        }
        [TestMethod]
        public void SavesJson()
        {
            var gtr = new ContractGetter(_crepo);
            var result = gtr.SaveJson("Contract.json");
            Assert.IsTrue(result.Count > 0);
        }
    }
}

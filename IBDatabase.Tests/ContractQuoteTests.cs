using System;
using System.Linq;
using IBDatabase.Redis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;

namespace IBDatabase.Tests
{
    /// <summary>
    /// !!!! REALLY IMPORTANT !!!!
    /// THE cacheClient creates a ConnectionMultiplexer 
    /// The connection to the Azure Redis Cache is managed by the ConnectionMultiplexer class. 
    /// This class is designed to be shared and reused throughout your client application, 
    /// and should not be created on a per operation basis.
    /// 
    /// The cacheClient actually creates 2 clients, one for the query and 1 for PING.  You can see
    /// this behavior if you open a redis-cli.exe and type CLIENT LIST while stopped in debug mode
    /// after the cacheClient is created.  (you will see 3 clients including the one you started to 
    /// query the CLIENT LIST.
    /// 
    /// That is why the base class for Getter* uses the Singleton pattern in the 
    /// </summary>
    [TestClass]
    public class ContractQuoteTests
    {

        private const string Connectionstring = "localhost";
        
        private ContractQuoteConnectedRepository _repo;
        private DBContractsConnectedRepository _crepo;
        [TestInitialize]
        public void Initialize()
        {
            _repo = new ContractQuoteConnectedRepository();
            _crepo = new DBContractsConnectedRepository();
        }
        [TestMethod]
        public void GetsKeys()
        {

            var quoteGetter = new QuoteGetter(_repo, _crepo);
            var list = quoteGetter.GetKeys().ToList();
            System.Diagnostics.Debug.WriteLine(list.Count);
            Assert.IsTrue(list.Count > 0);
        }
        [TestMethod]
        public void GetsAContractQuoteWhenInCache()
        {
            
            var quoteGetter = new QuoteGetter(_repo, _crepo);
            var contractQuote = quoteGetter.Get(257271740);
            Assert.IsNotNull(contractQuote);
        }
        [TestMethod]
        public void GetsAContractQuoteWhenInOnlyInDb()
        {

            var quoteGetter = new QuoteGetter( _repo, _crepo);
            var contractQuote = quoteGetter.Get(257271740);
            Assert.IsNotNull(contractQuote);
        }
        [TestMethod]
        public void ReturnsNullWhenNotInCacheOrDb()
        {

            var quoteGetter = new QuoteGetter(_repo, _crepo);
            var contractQuote = quoteGetter.Get(257271740);
            Assert.IsNull(contractQuote);
        }

        [TestMethod]
        public void GetsContractQuoteDictionary()
        {
            var quoteGetter = new QuoteGetter( _repo, _crepo);
            var list = quoteGetter.GetDictionary();
            System.Diagnostics.Debug.WriteLine(list.Count);
            Assert.IsTrue(list.Count > 0);
        }
        [TestMethod]
        public void SavesContractQuotesToCsv()
        {
            //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Connectionstring);
            var quoteGetter = new QuoteGetter(_repo, _crepo);
            var result = quoteGetter.SaveCsv("ContractQuotesRedis.csv");
            Assert.IsTrue(result.Count > 0);
        }
        [TestMethod]
        public void SavesContractQuotesToJson()
        {
            //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Connectionstring);
            var quoteGetter = new QuoteGetter(_repo, _crepo);
            var result = quoteGetter.SaveJson("ContractQuotesRedis.json");
            Assert.IsTrue(result.Count > 0);
        }

    }
}

using IBDatabase.Models;
using IBUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IBDatabase.Tests
{
    [TestClass]
    public class ContractQuoteConnectedRepositoryTests
    {
        [TestMethod]
        public void GetsContractQuotes()
        {
            ContractQuoteConnectedRepository repo = new ContractQuoteConnectedRepository();
            var result = repo.Get();
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void Crud()
        {
            decimal bidYield = 33.3M;
            ContractQuote contract = GetSample();
            ContractQuoteConnectedRepository repo = new ContractQuoteConnectedRepository();
            var c1 = repo.GetByConId(contract.ConId);

            // Test Delete
            if (c1 != null)
            {
                var removed = repo.Delete(c1);
                Assert.IsNull(removed);
            }
            // Test Create
            c1 = repo.Add(contract);
            Assert.IsNotNull(c1);

            var list = repo.Get();
            Assert.IsTrue(list.Count > 0);

            // Test Update
            c1.bidYield = bidYield;
            var c2 = repo.Update(c1);
            Assert.IsNotNull(c2);
            Assert.IsTrue(c2.bidYield == bidYield);

            // Test Read
            c1 = repo.GetByConId(contract.ConId);
            Assert.IsNotNull(c1);
            Assert.IsTrue(c1.bidYield == bidYield);

            // Delete the test record
            repo.Delete(c1);

            // Test Delete
            c2 = repo.GetByConId(contract.ConId);
            Assert.IsNull(c2);

        }

        #region "Test Helpers"
        private static ContractQuote GetSample()
        {
            IO.SetBasepath(@"C:\Users\bizca\Documents\Visual Studio 2015\Projects\IBDatabase\IBDatabase.Tests\TestData\");
            var contractList = IO.GetContractQuotesList("ContractQuotes.json");
            
            return contractList;
        }


        #endregion
    }
}

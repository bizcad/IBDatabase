using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using IBDatabase.Models;
using IBUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using StackExchange.Redis;

namespace IBDatabase.Tests
{
    [TestClass]
    public class DBContractConnectedRepositoryTests
    {
        #region "Individual methods subsubsumed by Crud

        [TestMethod]
        public void GetsContractList()
        {
            DBContractsConnectedRepository repo = new DBContractsConnectedRepository();            
            var result = repo.Get();
            Assert.IsTrue(result.Count > 0);
        }

        //[TestMethod]
        //public void DeletesContract()
        //{
        //    ContractsConnectedRepository repo = new ContractsConnectedRepository();
        //    var contract = GetSampleContract();
        //    var result = repo.Delete(contract);
        //    Assert.IsTrue(result.ConId == contract.ConId);
        //    var removed = repo.GetById(contract.Id);
        //    Assert.IsNull(removed);
        //}
        //[TestMethod]
        //public void InsertsContract()
        //{
        //    var contract = GetSampleContract();
        //    ContractsConnectedRepository repo = new ContractsConnectedRepository();
        //    var result = repo.Add(contract);
        //    Assert.IsTrue(result.Id != 0);
        //    Assert.IsTrue(result.ConId == contract.ConId);
        //}
        #endregion

        [TestMethod]
        public void Crud()
        {
            //string primaryExchange = "";
            //var contract = GetSampleContract();
            //DBContractsConnectedRepository repo = new DBContractsConnectedRepository();
            //var c1 = repo.GetByConId(contract.ConId);

            //// Test Delete
            //if (c1 != null)
            //{
            //    var removed = repo.Delete(c1);
            //    Assert.IsNull(removed);
            //}
            //// Test Create
            //c1 = repo.Add(contract);
            //Assert.IsNotNull(c1);

            //var list = repo.Get();
            //Assert.IsTrue(list.Count > 0);

            //// Test Update
            //c1.PrimaryExch = primaryExchange;
            //var c2 = repo.Update(c1);
            //Assert.IsNotNull(c2);
            //Assert.IsTrue(c2.PrimaryExch == primaryExchange);

            //// Test Read
            //c1 = repo.GetByConId(contract.ConId);
            //Assert.IsNotNull(c1);
            //Assert.IsTrue(c1.PrimaryExch == primaryExchange);

            //// Delete the test record
            //repo.Delete(c1);

            //// Test Delete
            //c2 = repo.GetByConId(contract.ConId);
            //Assert.IsNull(c2);

        }

        [TestMethod]
        public void InsertsContracts()
        {
            //var contracts = IO.GetDBContractDictionaryJson("ContractsDictionary.json");
            //DBContractsConnectedRepository repo = new DBContractsConnectedRepository();

            //foreach (var c in contracts)
            //{
            //    try
            //    {
            //        var contract = repo.Add(c.Value);
            //        Assert.IsNotNull(contract);
            //        Assert.IsTrue(c.Value.ConId == contract.ConId);
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug.WriteLine(ex.Message);
            //    }

            //}
            //var c1 = repo.Get();
            //Assert.IsNotNull(c1);
            //Assert.IsTrue(c1.Count > 0);
        }

        //[TestMethod]
        //public void GetsLaterThanToday()
        //{
        //    DBContractsConnectedRepository repo = new DBContractsConnectedRepository();
        //    var ltt = repo.LaterThanToday();
        //    Assert.IsTrue(ltt == "20170616");
        //}
        [TestMethod]
        public void GetsListOfContractsLaterThanToday()
        {
            DBContractsConnectedRepository repo = new DBContractsConnectedRepository();
            var ltt = repo.GetListLaterThanToday();
            Assert.IsTrue(ltt.Count > 0);
        }
        #region "Test Helpers"
        //private static DBContract GetSampleContract()
        //{
        //    IO.SetBasepath(@"C:\Users\bizca\Documents\Visual Studio 2015\Projects\IBDatabase\IBDatabase.Tests\TestData\");
        //    var contractList = IO.GetContractList("Contract.json");
        //    IBApi.Contract contract = contractList.Find(n => n.ConId == 206848474);
        //    return ContractConverter.ContractToDBContract(contract);
        //}

        #endregion
    }
}

using System.Collections.Generic;
using System.Linq;
using IBApi;
using IBDatabase.Models;
using IBUtility;
using StackExchange.Redis;

namespace IBDatabase.Redis
{

    public class ContractGetter : GetterBase, IGetter<DBContract>
    {
        private const string Basekey = "contract:";
        private DBContractsConnectedRepository _repo;
        public ContractGetter(DBContractsConnectedRepository repo)
        {
            _repo = repo;
        }
        public DBContract Set(DBContract contract)
        {
            string rediskey = $"{Basekey}{contract.ConId}";
            Client.Add(rediskey, contract);
            return contract;
        }
        public DBContract Get(long conId)
        {
            string rediskey = $"{Basekey}{conId}";
            var quote = Client.Get<DBContract>(rediskey);
            if (quote != null)
                return quote;

            
            DBContract cq = _repo.GetByConId((int)conId);
            if (cq != null)
            {
                Client.Add(rediskey, cq);
                return cq;
            }
            return null;
        }

        public IEnumerable<string> GetKeys()
        {
            return Client.SearchKeys("contract:*");
        }
        public Dictionary<string, DBContract> GetDictionary()
        {
            var dic = new Dictionary<string, DBContract>();
            foreach (string key in GetKeys())
            {
                DBContract item = Client.Get<DBContract>(key);
                dic.Add(key, item);
            }

            return dic;
        }

        public List<DBContract> GetList()
        {
            var list = new List<DBContract>();
            foreach (string key in Client.SearchKeys("contract:*"))
            {
                DBContract item = Client.Get<DBContract>(key);
                list.Add(item);
            }
            return list;
        }
        public List<DBContract> GetListFromContractList(List<Contract> contractList )
        {
            var list = new List<DBContract>();

            foreach (Contract contract in contractList)
            {
                string key = "contract:" + contract.ConId;
                DBContract item = Client.Get<DBContract>(key);
                list.Add(item);
            }
            SaveCsvFromList(list, "ContractQuotes.csv");
            return list;
        }
        public Dictionary<string, DBContract> SaveJson(string whereSerialized)
        {
            var dic = GetDictionary();
            IO.SerializeJson(dic, whereSerialized);
            return dic;
        }

        public List<DBContract> SaveCsv(string whereSerialized)
        {
            List<DBContract> list = GetList();
            var slist = IBDatabase.CsvSerializer.Serialize(",", list);
            IO.WriteStringList(slist, whereSerialized);
            return list;
        }
        public List<DBContract> SaveCsvFromList(List<DBContract> list, string whereSerialized)
        {
            var slist = IBDatabase.CsvSerializer.Serialize(",", list);
            IO.WriteStringList(slist, whereSerialized);
            return list;
        }

        private List<Contract> ReadContracts(string whereSerialized)
        {
            return IO.GetContractList("Contract.json");
        }
    }
}

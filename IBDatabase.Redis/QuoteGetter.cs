using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBDatabase.Factory;
using IBDatabase.Models;
using IBUtility;
using StackExchange.Redis;

namespace IBDatabase.Redis
{
    public class QuoteGetter : GetterBase, IGetter<ContractQuote>
    {
        private const string Basekey = "contractquote:";
        private readonly ContractQuoteConnectedRepository _quoteRepository;
        private readonly DBContractsConnectedRepository _contractRepository;
        
        public QuoteGetter(ContractQuoteConnectedRepository repository, DBContractsConnectedRepository contractRepository)
        {
            _quoteRepository = repository;
            _contractRepository = contractRepository;
        }
        public ContractQuote Set(ContractQuote contract)
        {
            string rediskey = $"{Basekey}{contract.ConId}";
            Client.Add(rediskey, contract);
            return contract;
        }

        public async Task<ContractQuote> GetAsync(long conId)
        {
            string rediskey = $"{Basekey}{conId}";
            return await Client.GetAsync<ContractQuote>(rediskey);
        }
        public ContractQuote Get(long conId)
        {
            // Create a key
            string rediskey = $"{Basekey}{conId}";

            // Check Cache first
            var quote = Client.Get<ContractQuote>(rediskey);
            if (quote != null)
                return quote;

            
            

            // Check Database to see if the quote is there
            
            ContractQuote cq = ContractQuoteFactory.Create(_contractRepository.GetByConId((int)conId));
            if (cq != null)
            {
                Client.Add(rediskey, cq);
                return cq;
            }

            // As a last resort create quote from the contract
            //  Like this getter, the ContractGetter will create the contract and save it to redis
            ContractGetter getter = new ContractGetter(_contractRepository);
            var dbcontract = getter.Get(conId);
            if (dbcontract == null) return null;

            return ContractQuoteFactory.Create(dbcontract);

        }
        public IEnumerable<string> GetKeys()
        {
            return Client.SearchKeys($"{Basekey}*");
        }
        public Dictionary<string, ContractQuote> GetDictionary()
        {
            var dic = new Dictionary<string, ContractQuote>();
            foreach (string key in GetKeys())
            {
                ContractQuote item = Client.Get<ContractQuote>(key);
                dic.Add(key, item);
            }

            return dic;
        }
        public List<ContractQuote> GetList()
        {
            var list = new List<ContractQuote>();
            foreach (string key in GetKeys())
            {
                ContractQuote item = Client.Get<ContractQuote>(key);
                list.Add(item);
            }
            return list;
        }
        public Dictionary<string, ContractQuote> SaveJson(string whereSerialized)
        {
            var dic = GetDictionary();
            IO.SaveContractQuotesJson(dic, whereSerialized);
            return dic;
        }
        public List<ContractQuote> SaveCsv(string whereSerialized)
        {
            List<ContractQuote> list = GetList().ToList();
            var slist = CsvSerializer.Serialize(",", list);
            IO.WriteStringList(slist, whereSerialized);
            return list;
        }
        public List<ContractQuote> SaveCsv(List<ContractQuote> list, string whereSerialized)
        {
           
            var slist = CsvSerializer.Serialize(",", list);
            IO.WriteStringList(slist, whereSerialized);
            return list;
        }


    }
}

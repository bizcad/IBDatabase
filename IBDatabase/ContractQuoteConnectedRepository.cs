using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBDatabase.Models;

namespace IBDatabase
{
    public class ContractQuoteConnectedRepository : IConnectedRepository<ContractQuote>
    {
        private readonly BizcadIBContext _context = new BizcadIBContext();

        public List<ContractQuote> Get()
        {
            return _context.ContractQuotes.ToList();
        }
        public ContractQuote GetById(int id)
        {
            return _context.ContractQuotes.Find(id);
        }
        public List<ContractQuote> GetForConId(int conid)
        {
            return _context.ContractQuotes.Where(c => c.ConId == conid).ToList();
        }
        /// <summary>
        /// Removes a contractQuote from the database if it exists
        /// </summary>
        /// <param name="contractQuote">the contractQuote to remove</param>
        /// <returns>the ContractQuote removed</returns>
        public ContractQuote Delete(ContractQuote contractQuote)
        {
            if (contractQuote.Id == 0)
            {
                var c = _context.ContractQuotes.FirstOrDefault(r => r.ConId == contractQuote.ConId);
                if (c != null)
                {
                    contractQuote.Id = c.Id;
                    _context.ContractQuotes.Remove(c);
                }
            }
            else
            {
                var c = _context.ContractQuotes.Find(contractQuote.Id);
                if (c != null)
                {
                    _context.ContractQuotes.Remove(c);
                }
            }

            Save();
            return contractQuote;

        }

        public void Save()
        {
            var ret = _context.SaveChanges();
        }
        public ContractQuote Update(ContractQuote contractQuote)
        {
            return InsertUpdate(contractQuote);
        }
        public ContractQuote Add(ContractQuote contractQuote)
        {
            return InsertUpdate(contractQuote);
        }
        /// <summary>
        /// Inserts or Updates a ContractQuote in the database.
        /// </summary>
        /// <param name="contractQuote">The ContractQuote with the new values</param>
        /// <returns>The updated ContractQuote</returns>
        public ContractQuote InsertUpdate(ContractQuote contractQuote)
        {
            var c = _context.ContractQuotes.FirstOrDefault(r => r.ConId == contractQuote.ConId);
            if (c == null)
            {
                c = _context.ContractQuotes.Add(contractQuote);
                Save();
                return c;
            }
            contractQuote.Id = c.Id;
            _context.Entry(c).CurrentValues.SetValues(contractQuote);
            Save();
            return contractQuote;
        }

        public ContractQuote GetByConIdAndDate(int conid, string shortHistoricalDate)
        {
            throw new NotImplementedException();
        }

        public ContractQuote GetByConId(int conid)
        {
            return _context.ContractQuotes.FirstOrDefault(c => c.ConId == conid);
        }

        //public ContractQuote UpdateField(ContractQuote c, string fieldName)
        //{
        //    string connectionstring = _context.Database.Connection.ConnectionString;
        //    SqlConnection conn = new SqlConnection(connectionstring);
        //    SqlCommand updateFieldCommand = new SqlCommand("");
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBDatabase.Models;

namespace IBDatabase
{
    public class DBContractsConnectedRepository : IConnectedRepository<DBContract>
    {
        private readonly BizcadIBContext _context = new BizcadIBContext();

        public List<DBContract> Get()
        {
            return _context.DbContracts.ToList();
        }
        public DBContract GetById(int id)
        {
            return _context.DbContracts.Find(id);
        }
        public DBContract GetByConId(int conid)
        {
            return _context.DbContracts.FirstOrDefault(c => c.ConId == conid);
        }
        public List<DBContract> GetListLaterThanToday()
        {
            string x = LaterThanToday();
            var y = _context.DbContracts.Where(n => string.Compare(n.Expiry, x, StringComparison.Ordinal) > 0);
            return y.ToList();
        }
        /// <summary>
        /// Makes a string in DBContract.Expiry format starting with tomorrow
        /// </summary>
        /// <returns>The date as yyyymmdd starting tomorrow</returns>
        public string LaterThanToday()
        {
            DateTime now = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            sb.Append(now.Year);
            if (now.Month < 10)
                sb.Append("0");
            sb.Append(now.Month);
            if (now.Day < 10)
                sb.Append("0");
            sb.Append(now.Day);
            return sb.ToString();
        }
        /// <summary>
        /// Removes a contract from the database if it exists
        /// </summary>
        /// <param name="dBContract">the contract to remove</param>
        /// <returns>the DBContract removed</returns>
        public DBContract Delete(DBContract dBContract)
        {
            if (dBContract.Id == 0)
            {
                var c = _context.DbContracts.FirstOrDefault(r => r.ConId == dBContract.ConId);
                if (c != null)
                {
                    dBContract.Id = c.Id;
                    _context.DbContracts.Remove(c);
                }
            }
            else
            {
                var c = _context.DbContracts.Find(dBContract.Id);
                if (c != null)
                {
                    _context.DbContracts.Remove(c);
                }
            }

            Save();
            return dBContract;

        }

        public void Save()
        {
            var ret = _context.SaveChanges();
        }
        
        public DBContract Update(DBContract dBContract)
        {
            return InsertUpdate(dBContract);
        }
        public DBContract Add(DBContract dBContract)
        {
            try
            {
                var d = InsertUpdate(dBContract);
                Save();
                return d;
            }

            catch (System.Data.SqlClient.SqlException sdex)
            {
                throw new Exception(sdex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //return InsertUpdate(contract);
        }
        /// <summary>
        /// Inserts or Updates a DBContract in the database.
        /// </summary>
        /// <param name="dBContract">The DBContract with the new values</param>
        /// <returns>The updated DBContract</returns>
        public DBContract InsertUpdate(DBContract dBContract)
        {
            try
            {
                var c = _context.DbContracts.FirstOrDefault(r => r.ConId == dBContract.ConId);
                if (c == null)
                {
                    var r = _context.DbContracts.Add(dBContract);
                    Save();
                    return r;
                }
                dBContract.Id = c.Id;
                _context.Entry(c).CurrentValues.SetValues(dBContract);
            }
            catch (System.Data.SqlClient.SqlException sdex)
            {
                throw new Exception(sdex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            Save();
            return dBContract;
        }

        public List<DBContract> GetForConId(int conid)
        {
            throw new NotImplementedException();
        }

        public DBContract GetByConIdAndDate(int conid, string shortHistoricalDate)
        {
            throw new NotImplementedException();
        }
    }
}


using IBDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBDatabase
{
    public class DBContractDetailsConnectedRepository : IConnectedRepository<DBContractDetail>
    {
        private readonly BizcadIBContext _context = new BizcadIBContext();

        public DBContractDetail Add(DBContractDetail row)
        {
            try
            {
                var d = InsertUpdate(row);
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
        }

        public DBContractDetail Delete(DBContractDetail row)
        {
            if (row.ConId == 0)
            {
                var c = _context.DbContractDetails.FirstOrDefault(r => r.ConId == row.ConId);
                if (c != null)
                {
                    row.ConId = c.Id;
                    _context.DbContractDetails.Remove(c);
                }
            }
            else
            {
                var c = _context.DbContractDetails.Find(row.ConId);
                if (c != null)
                {
                    _context.DbContractDetails.Remove(c);
                }
            }

            Save();
            return row;
        }

        public List<DBContractDetail> Get()
        {
            return _context.DbContractDetails.ToList();
        }

        public DBContractDetail GetByConId(int conid)
        {
            return _context.DbContractDetails.FirstOrDefault(c => c.ConId == conid);
        }

        public DBContractDetail GetByConIdAndDate(int conid, string shortHistoricalDate)
        {
            throw new NotImplementedException();
        }

        public DBContractDetail GetById(int id)
        {
            return _context.DbContractDetails.FirstOrDefault(c => c.ConId == id);
        }

        public List<DBContractDetail> GetForConId(int conid)
        {
            throw new NotImplementedException();
        }

        public DBContractDetail InsertUpdate(DBContractDetail row)
        {
            var c = _context.DbContractDetails.FirstOrDefault(r => r.ConId == row.ConId);
            try
            {
                if (c == null)
                {
                    var r = _context.DbContractDetails.Add(row);
                    Save();
                    return r;
                }
                _context.Entry(c).Entity.TradingHours = row.TradingHours;
                _context.Entry(c).Entity.LiquidHours = row.LiquidHours;
                //_context.Entry(c).Entity.Summary = row.Summary;

                // Results in a Exception because the Id (Indentity columm) cannot be updated.
                //System.Data.Entity.Infrastructure.DbPropertyValues cv = _context.Entry(c).CurrentValues.SetValues(row);
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
            return c;
        }

        public void Save()
        {
            var ret = _context.SaveChanges();
        }

        public DBContractDetail Update(DBContractDetail row)
        {
            return InsertUpdate(row);
        }
    }
}

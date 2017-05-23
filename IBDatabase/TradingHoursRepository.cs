using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBDatabase.Models;

namespace IBDatabase
{
    public class TradingHoursRepository : IConnectedRepository<ContractTradingHour>
    {
        private readonly BizcadIBContext _context = new BizcadIBContext();

        public ContractTradingHour Add(ContractTradingHour row)
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

        public ContractTradingHour Delete(ContractTradingHour row)
        {
            if (row.ConId == 0)
            {
                var c = _context.ContractTradingHours.FirstOrDefault(r => r.ConId == row.ConId);
                if (c != null)
                {
                    row.ConId = c.Id;
                    _context.ContractTradingHours.Remove(c);
                }
            }
            else
            {
                var c = _context.ContractTradingHours.Find(row.ConId);
                if (c != null)
                {
                    _context.ContractTradingHours.Remove(c);
                }
            }

            Save();
            return row;
        }

        public List<ContractTradingHour> Get()
        {
            return _context.ContractTradingHours.ToList();
        }

        public List<ContractTradingHour> GetForConId(int conid)
        {
            return _context.ContractTradingHours.Where(c => c.ConId == conid).ToList();
        }
        public ContractTradingHour GetByConIdAndDate(int conid, string shortHistoricalDate)
        {
            return _context.ContractTradingHours.FirstOrDefault(r => r.ConId == conid && r.ShortHistoricalDate == shortHistoricalDate);
        }

        public ContractTradingHour GetById(int id)
        {
            return _context.ContractTradingHours.Find(id);
        }

        public ContractTradingHour InsertUpdate(ContractTradingHour row)
        {
            var c = _context.ContractTradingHours.FirstOrDefault(r => r.ConId == row.ConId && r.ShortHistoricalDate == row.ShortHistoricalDate);
            try
            {
                if (c == null)
                {
                    var r = _context.ContractTradingHours.Add(row);
                    var x = _context.ContractTradingHours.Where(s => s.ConId == row.ConId && s.ShortHistoricalDate == row.ShortHistoricalDate);
                    Save();
                    return r;
                }
                _context.Entry(c).Entity.TradingHours = row.TradingHours;
                _context.Entry(c).Entity.LiquidHours = row.LiquidHours;
                _context.Entry(c).Entity.GmtOffset = row.GmtOffset;
                _context.Entry(c).Entity.TimeZoneId = row.TimeZoneId;
                _context.Entry(c).Entity.OpeningBell = row.OpeningBell;
                _context.Entry(c).Entity.ClosingBell = row.ClosingBell;

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

        public ContractTradingHour Update(ContractTradingHour row)
        {
            return InsertUpdate(row);
        }

        public ContractTradingHour GetByConId(int conid)
        {
            return _context.ContractTradingHours.FirstOrDefault(c => c.ConId == conid);
        }
    }
}

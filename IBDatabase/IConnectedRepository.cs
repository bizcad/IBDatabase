using System.Collections.Generic;
using IBDatabase.Models;

namespace IBDatabase
{
    public interface IConnectedRepository<T>
    {
        T Add(T row);
        T Delete(T row);
        List<T> GetForConId(int conid);
        T GetByConId(int conid);
        T GetByConIdAndDate(int conid, string shortHistoricalDate);
        T GetById(int id);
        List<T> Get();
        T InsertUpdate(T row);
        void Save();
        T Update(T row);
    }
}
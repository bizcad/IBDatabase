using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBUtility
{
    public interface ICsvReader<T>
    {
        List<T> DeserializeRecords(string whereSerialized);
        List<T> DeserializeNumberOfRecords(string whereSerialized, int number);
        T DeserializeFirstRecord(string whereSerialized);
        string GetHeaderLine(string whereSerialized);
    }
}

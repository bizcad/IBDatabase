using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBDatabase.Models;

namespace IBDatabase.Factory
{
    public static class TickGenericFactory
    {
        public static TickGeneric Create(dynamic y)
        {
            TickGeneric s = new TickGeneric
            {
                Value = y.Value,
                Id = y.Id,
                ConId = y.ConId,
                LocalTransactionTime = y.LocalTransactionTime,
                TickerId = y.TickerId,
                TickType = y.TickType,
                TickTypeName = y.TickTypeName
            };
            return s;
        }
    }

}

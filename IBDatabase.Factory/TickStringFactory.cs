using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBDatabase.Models;

namespace IBDatabase.Factory
{
    public static class TickStringFactory
    {
        public static TickString Create(dynamic y)
        {
            TickString s = new TickString
            {
                LastTimestamp = y.LastTimestamp,
                Value = y.Value,
                Id = y.Id,
                ConId = y.ConId,
                LocalTransactionTime = y.LocalTransactionTime,
                TickerId = y.TickerId,
                TickType = y.TickType,
                TickTypeName = y.TickTypeName
            };
            s.LocalTransactionTime = DateTime.Now;


            return s;
        }

    }

}

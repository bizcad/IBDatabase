using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBDatabase.Models;

namespace IBDatabase.Factory
{
    public static class TickPriceFactory
    {
        public static TickPrice Create(dynamic y)
        {
            TickPrice p = new TickPrice
            {
                CanAutoExecute = y.CanAutoExecute,
                Price = y.Price,
                Id = y.Id,
                ConId = y.ConId,
                LocalTransactionTime = y.LocalTransactionTime,
                TickerId = y.TickerId,
                TickType = y.TickType,
                TickTypeName = y.TickTypeName
            };
            return p;
        }
    }
}


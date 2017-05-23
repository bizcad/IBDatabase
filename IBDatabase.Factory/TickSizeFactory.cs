using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBDatabase.Models;

namespace IBDatabase.Factory
{
    public static class TickSizeFactory
    {
        public static TickSize Create(dynamic y)
        {
            TickSize size = new TickSize
            {
                Size = y.Size,
                Id = y.Id,
                ConId = y.ConId,
                LocalTransactionTime = y.LocalTransactionTime,
                TickerId = y.TickerId,
                TickType = y.TickType,
                TickTypeName = y.TickTypeName
            };

            return size;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBDatabase.Models;

namespace IBDatabase.Factory
{
    public static class TickOptionCompFactory
    {
        public static TickOptionComp Create(dynamic y)
        {
            TickOptionComp comp = new TickOptionComp
            {
                OptionPrice = y.OptionPrice,
                ImpliedVolatility = y.ImpliedVolatility,
                Delta = y.Delta,
                pvDividend = y.pvDividend,
                Gamma = y.Gamma,
                Vega = y.Vega,
                Theta = y.Theta,
                UnderlyingPrice = y.UnderlyingPrice,
                Id = y.Id,
                ConId = y.ConId,
                LocalTransactionTime = y.LocalTransactionTime,
                TickerId = y.TickerId,
                TickType = y.TickType,
                TickTypeName = y.TickTypeName
            };
            return comp;
        }
    }

}

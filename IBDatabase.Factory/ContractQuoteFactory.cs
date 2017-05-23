using System;
using IBApi;
using IBDatabase.Models;

namespace IBDatabase.Factory
{
    public class ContractQuoteFactory
    {
        public ContractQuote Create(long conId)
        {
            ContractQuote cq = new ContractQuote
            {
                ConId = Convert.ToInt32(conId),
                LocalTransactionTime = DateTime.Now
            };
            return cq;
        }
        public ContractQuote Create(int conId)
        {
            ContractQuote cq = new ContractQuote
            {
                ConId = conId,
                LocalTransactionTime = DateTime.Now
            };
            return cq;
        }
        public ContractQuote Create(string conId)
        {
            ContractQuote cq = new ContractQuote
            {
                ConId = Convert.ToInt32(conId),
                LocalTransactionTime = DateTime.Now
            };
            return cq;
        }
        public static ContractQuote Create(DBContract c)
        {
            ContractQuote cq = new ContractQuote
            {
                Strike = c.Strike,
                Expiry = c.Expiry,
                localSymbol = c.LocalSymbol,
                ContractName = c.LocalSymbol,
                Right = c.Right,
                ConId = c.ConId,
                LocalTransactionTime = DateTime.Now
            };

            var arr = c.LocalSymbol.Split(' ');
            cq.ContractName = arr[0].Trim();

            return cq;
        }
        public static ContractQuote Create(Contract c)
        {
            ContractQuote cq = new ContractQuote
            {
                Strike = c.Strike,
                Expiry = c.Expiry,
                localSymbol = c.LocalSymbol,
                ContractName = c.LocalSymbol,
                Right = c.Right,
                ConId = c.ConId,
                LocalTransactionTime = DateTime.Now
            };

            if (c.Right == null) return cq;

            var arr = c.LocalSymbol.Split(' ');
            cq.ContractName = arr[0].Trim();

            return cq;
        }
    }
}

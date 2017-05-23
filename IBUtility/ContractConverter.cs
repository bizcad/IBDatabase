using System;
using System.Collections.Generic;
using IBApi;
using IBDatabase.Models;
using Newtonsoft.Json;

namespace IBUtility
{
    public static class ContractConverter
    {
        /// <summary>
        /// Changes a IBApi.Contract into a DBContract
        /// </summary>
        /// <param name="c">The Contract</param>
        /// <returns>A new DBContract</returns>
        public static DBContract ContractToDBContract(Contract c)
        {
            DBContract d = new DBContract
            {
                ConId = c.ConId,
                ComboLegsDescription = c.ComboLegsDescription,
                Currency = c.Currency,
                Exchange = c.Exchange,
                Expiry = c.Expiry,
                IncludeExpired = c.IncludeExpired,
                LocalSymbol = c.LocalSymbol,
                Multiplier = c.Multiplier,
                PrimaryExch = c.PrimaryExch,
                Right = c.Right,
                Strike = c.Strike,
                SecId = c.SecId,
                SecIdType = c.SecIdType,
                SecType = c.SecType,
                Symbol = c.Symbol,
                TradingClass = c.TradingClass,
                UnderComp = JsonConvert.SerializeObject(c.UnderComp),
                ComboLegs = JsonConvert.SerializeObject(c.ComboLegs)
            };

            return d;
        }
        /// <summary>
        /// Changes a DBContract back into a Contract
        /// </summary>
        /// <param name="d">The DBContract retreived from storage</param>
        /// <returns>A new Contract</returns>
        public static Contract DBContractToContract(DBContract d)
        {
            Contract c = new Contract();
            try
            {
                c.ConId = d.ConId;
                c.ComboLegsDescription = d.ComboLegsDescription;
                c.Currency = d.Currency;
                c.Exchange = d.Exchange;
                c.Expiry = d.Expiry;
                c.IncludeExpired = d.IncludeExpired;
                c.LocalSymbol = d.LocalSymbol;
                c.Multiplier = d.Multiplier;
                c.PrimaryExch = d.PrimaryExch;
                c.Right = d.Right;
                c.Strike = d.Strike;
                c.SecId = d.SecId;
                c.SecIdType = d.SecIdType;
                c.SecType = d.SecType;
                c.Symbol = d.Symbol;
                c.TradingClass = d.TradingClass;
                if (d.UnderComp == null)
                    c.UnderComp = null;
                else
                    c.UnderComp = (UnderComp)JsonConvert.DeserializeObject(d.UnderComp);

                if (d.ComboLegs == null)
                    c.ComboLegs = null;
                else
                    c.ComboLegs = (List<ComboLeg>)JsonConvert.DeserializeObject(d.ComboLegs);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }

            return c;
        }
    }

}

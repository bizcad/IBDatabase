using IBDatabase.Models;

namespace IBDatabase.Factory
{
    public static class DBContractFactory
    {
        public static DBContract Create(dynamic y)
        {
            var db = new DBContract();
            db.Id = 0;
            db.ConId = y.ConId;
            db.Symbol = y.Symbol;
            db.SecType = y.SecType;
            db.Expiry = y.Expiry;
            db.Strike = y.Strike;
            db.Right = y.Right;
            db.Multiplier = y.Multiplier;
            db.Exchange = y.Exchange;
            db.Currency = y.Currency;
            db.LocalSymbol = y.LocalSymbol;
            db.PrimaryExch = y.PrimaryExch;
            db.TradingClass = y.TradingClass;
            db.IncludeExpired = y.IncludeExpired;
            db.SecIdType = y.SecTypeId;
            db.SecId = y.SecId;
            db.ComboLegsDescription = y.ComboLegsDescription;
            db.ComboLegs = y.ComboLegs;
            db.UnderComp = y.UnderComp;


            return db;

        }
    }
}

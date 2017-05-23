using System.Data.Entity;
using IBDatabase.Models;

namespace IBDatabase
{
    public class BizcadIBContext:DbContext
    {
        public DbSet<DBContract> DbContracts { get; set; }
        public DbSet<ContractQuote> ContractQuotes { get; set; }
        public DbSet<IncomingBar> IncomingBars { get; set; }
        public DbSet<DBContractDetail> DbContractDetails { get; set; }
        public DbSet<DBTagValue> DbTagValues { get; set; }
        public DbSet<InstantaneousTrendSerialization> InstantaneousTrendSerializations { get; set; }
        public DbSet<ContractTradingHour> ContractTradingHours { get; set; }
    }
}

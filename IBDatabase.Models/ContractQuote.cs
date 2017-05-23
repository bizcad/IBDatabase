using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBDatabase.Models
{
    public class ContractQuote
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [Index("IX_ContractQuoteConIdIndex")]
        public int ConId { get; set; }
        [MaxLength(512)]
        public string ContractName { get; set; }
        
        public double? Strike { get; set; }
        [MaxLength(10)]
        public string Expiry { get; set; }
        [MaxLength(4)]
        public string Right { get; set; }
        [MaxLength(128)]
        public string localSymbol { get; set; }
        public decimal? bidSize { get; set; }
        public decimal? bidPrice { get; set; }
        public decimal? askPrice { get; set; }
        public decimal? askSize { get; set; }
        public decimal? lastPrice { get; set; }
        public decimal? lastSize { get; set; }
        public decimal? high { get; set; }
        public decimal? low { get; set; }
        public int volume { get; set; }
        public decimal? close { get; set; }
        public decimal? bidOptComp { get; set; }
        public decimal? askOptComp { get; set; }
        public decimal? lastOptComp { get; set; }
        public decimal? modelOptComp { get; set; }
        public decimal? open { get; set; }
        public decimal? WeekLow13 { get; set; }
        public decimal? WeekHigh13 { get; set; }
        public decimal? WeekLow26 { get; set; }
        public decimal? WeekHigh26 { get; set; }
        public decimal? WeekLow52 { get; set; }
        public decimal? WeekHigh52 { get; set; }
        public decimal? AvgVolume { get; set; }
        public decimal? OpenInterest { get; set; }
        public decimal? OptionHistoricalVolatility { get; set; }
        public decimal? OptionImpliedVolatility { get; set; }
        public decimal? OptionBidExchStr { get; set; }
        public decimal? OptionAskExchStr { get; set; }
        public decimal? OptionCallOpenInterest { get; set; }
        public decimal? OptionPutOpenInterest { get; set; }
        public decimal? OptionCallVolume { get; set; }
        public decimal? OptionPutVolume { get; set; }
        public decimal? IndexFuturePremium { get; set; }
        public decimal? bidExch { get; set; }
        public decimal? askExch { get; set; }
        public decimal? auctionVolume { get; set; }
        public decimal? auctionPrice { get; set; }
        public decimal? auctionImbalance { get; set; }
        public decimal? markPrice { get; set; }
        public decimal? bidEFP { get; set; }
        public decimal? askEFP { get; set; }
        public decimal? lastEFP { get; set; }
        public decimal? openEFP { get; set; }
        public decimal? highEFP { get; set; }
        public decimal? lowEFP { get; set; }
        public decimal? closeEFP { get; set; }
        public long lastTimestamp { get; set; }
        public decimal? shortable { get; set; }
        public decimal? fundamentals { get; set; }
        public decimal? RTVolume { get; set; }
        public int halted { get; set; }
        public decimal? bidYield { get; set; }
        public decimal? askYield { get; set; }
        public decimal? lastYield { get; set; }
        public decimal? custOptComp { get; set; }
        public decimal? trades { get; set; }
        public decimal? trades_min { get; set; }
        public decimal? volume_min { get; set; }
        public decimal? lastRTHTrade { get; set; }
        public decimal? regulatoryImbalance { get; set; }
        public DateTime LocalTransactionTime { get; set; }
        public DBContract DbContract { get; set; }
    }

}

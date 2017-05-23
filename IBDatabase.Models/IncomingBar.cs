using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBDatabase.Models
{
    public class IncomingBar
    {
        /// <summary>
        /// The Id provided by the database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The ticker ID of the request to which this bar is responding.
        /// </summary>
        /// <summary>
        /// A string representation of the name of this class
        /// </summary>
        public string EntityType { get; set; }
        public int ReqId { get; set; }
        /// <summary>
        /// The date-time stamp of the start of the bar. 
        /// The format is determined by the reqHistoricalData() formatDate parameter.
        ///  The unix timestamp for the quote as GMT
        /// </summary>        
        public long Time { get; set; }

        /// <summary>
        /// The start time of the bar in DateTime Format.
        /// </summary>
        public DateTime BarStartTime { get; set; }
        /// <summary>
        /// The contract Id to which the bar applies.  This value is derived from the 
        /// ContractTranslationDictionary which translates the reqId into the conId.
        /// The dictionary is generated at request time.
        /// </summary>
        public int ConId { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public long Volume { get; set; }
        /// <summary>
        /// The weighted average price during the time covered by the bar.
        /// </summary>
        public double Wap { get; set; }
        /// <summary>
        ///  When TRADES historical data is returned, represents the number of trades that 
        ///     occurred during the time period the bar covers.
        /// </summary>
        public int Count { get; set; }
    }
}

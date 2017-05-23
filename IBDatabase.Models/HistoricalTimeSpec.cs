/*  
 *  This class is to hold a spec for generating a historical data fetch from IB
 *  The proerties are the parameters for IB's api call to reqHistoricalData
 *  https://www.interactivebrokers.com/en/software/api/apiguide/java/reqhistoricaldata.htm
 *  
 *  Here is the spec
 *  There are some limits to how much data can be requested at the bottom of the page
 *  https://www.interactivebrokers.com/en/software/api/api_Left.htm#CSHID=apiguide%2Fjava%2Freqhistoricaldata.htm|StartTopic=apiguide%2Fjava%2Freqhistoricaldata.htm|SkinName=ibskin
 *  Limits:
 *  https://www.interactivebrokers.com/en/software/api/apiguide/tables/historical_data_limitations.htm
 *  
 *  The call for historical data starts at the EndDateTime - DurationString converted to a DateTimeOffset
 *  
 *  It gets whatever data is requested.
 *  Determines the nature of data being extracted. Valid values include:  
 *  TRADES
 *  MIDPOINT 
 *  BID
 *  ASK
 *  BID_ASK
 *  HISTORICAL_VOLATILITY
 *  OPTION_IMPLIED_VOLATILITY
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBDatabase.Models
{
    public class HistoricalTimeSpec
    {
        /// <summary>
        /// The start time of the last bar of the historical data.
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// The What to show (usually TRADES)
        /// </summary>
        public string WhatToShow { get; set; }

        /// <summary>
        /// The duration of the historical fetch.  See Limitations
        /// </summary>
        public string DurationString { get; set; }

        /// <summary>
        /// The valid bar size for the request.  See Limitations
        /// </summary>
        public string BarSizeSetting { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBDatabase.Models
{
    public class TickPrice
    {
        /// <summary>
        /// The Id for use with a database as the index
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Contract Id
        /// </summary>
        public int ConId { get; set; }
        /// <summary>
        /// The local transaction time
        /// </summary>
        public DateTime LocalTransactionTime { get; set; }
        /// <summary>
        /// The ticker ID that was specified previously in the call to reqMktData()
        /// </summary>
        public int TickerId { get; set; }
        /// <summary>
        /// 	Specifies the type of tick
        /// </summary>
        public int TickType { get; set; }
        /// <summary>
        /// 	Specifies the name of the tick type, although it could be looked up in TickType class
        ///  </summary>
        public string TickTypeName { get; set; }
        public decimal Price { get; set; }
        public int CanAutoExecute { get; set; }
    }
}

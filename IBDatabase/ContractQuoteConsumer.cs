﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using IBApi;
using IBDatabase.Factory;
using IBDatabase.Models;
using IBUtility;
using Newtonsoft.Json;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;

namespace IBDatabase
{
    public class ContractQuoteConsumer
    {
        public int Counter = 0;
        DateTime start = DateTime.Now;
        public List<ContractQuote> quotelist { get; set; }
        private Dictionary<string, ContractQuote> quotes;
        public IDatabase Db { get; set; }
        private readonly StackExchangeRedisCacheClient _cacheClient;
        public ContractQuoteConsumer()
        {
            quotelist = new List<ContractQuote>();
            Db = RedisConnectorHelper.Connection.GetDatabase();
        }

        public async Task<string> ConsumeAsync(BufferBlock<string> queue)
        {
            var json = await queue.ReceiveAsync();
            KeyValuePair<int, object> kvp;
            try
            {
                kvp =
                   (KeyValuePair<int, object>)
                       Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(KeyValuePair<int, object>));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            ContractQuote quote = new ContractQuote();
            quote.Id = Counter;
            dynamic y = (dynamic)kvp.Value;
            //string s = (string) kvp.Value;
            //TickPrice price;
            //TickSize size;
            //TickOptionComp optionComp;
            #region "Switch"
            switch (kvp.Key)
            {
                case TickType.BID:
                    //price = (TickPrice)JsonConvert.DeserializeObject(y, typeof(TickPrice));
                    //price = (TickPrice)y;
                    quote.bidPrice = y.Price;
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;
                case TickType.ASK:
                    // price = (TickPrice)JsonConvert.DeserializeObject(y, typeof(TickPrice));
                    // price = (TickPrice)y;
                    //price = (TickPrice)JsonConvert.DeserializeObject(json, typeof(KeyValuePair<int, TickPrice>));
                    quote.askPrice = y.Price;
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;
                case TickType.LAST:
                    // price = (TickPrice)JsonConvert.DeserializeObject(y, typeof(TickPrice));
                    // price = (TickPrice)y;
                    //price = (TickPrice)JsonConvert.DeserializeObject(json, typeof(KeyValuePair<int, TickPrice>));
                    quote.lastPrice = y.Price;
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;
                case TickType.HIGH:
                    // price = (TickPrice)y;
                    //price = (TickPrice)JsonConvert.DeserializeObject(json, typeof(KeyValuePair<int, TickPrice>));
                    quote.high = y.Price;
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;
                case TickType.LOW:
                    // price = (TickPrice)y;
                    //price = (TickPrice)JsonConvert.DeserializeObject(json, typeof(KeyValuePair<int, TickPrice>));
                    quote.low = y.Price;
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;
                case TickType.CLOSE:
                    // price = (TickPrice)y;
                    //price = (TickPrice)JsonConvert.DeserializeObject(json, typeof(KeyValuePair<int, TickPrice>));
                    quote.close = y.Price;
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;
                case TickType.OPEN:
                    // price = (TickPrice)y;
                    //price = (TickPrice)JsonConvert.DeserializeObject(json, typeof(KeyValuePair<int, TickPrice>));
                    quote.open = y.Price;
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;

                case TickType.BID_SIZE:
                    // size = (TickSize)JsonConvert.DeserializeObject(y, typeof(TickSize));
                    // size = (TickSize)y;
                    //size = (TickSize)JsonConvert.DeserializeObject(json, typeof(KeyValuePair<int, TickSize>));
                    quote.bidSize = y.Size;
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;

                case TickType.ASK_SIZE:
                    // size = (TickSize)y;
                    //size = (TickSize)JsonConvert.DeserializeObject(json, typeof(KeyValuePair<int, TickSize>));
                    quote.askSize = y.Size;
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;

                case TickType.LAST_SIZE:
                    // size = (TickSize)y;
                    //size = (TickSize)JsonConvert.DeserializeObject(json, typeof(KeyValuePair<int, TickSize>));
                    quote.lastSize = y.Size;
                    quote.ConId = y.ConId;
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    break;
                case TickType.VOLUME:
                    // size = (TickSize)y;
                    //size = (TickSize)JsonConvert.DeserializeObject(json, typeof(KeyValuePair<int, TickSize>));
                    quote.volume = y.Size;
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;


                case TickType.LAST_TIMESTAMP:
                    //TickString tickString = (TickString)y;
                    quote.lastTimestamp = Convert.ToInt64(y.LastTimestamp);
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;

                case TickType.HALTED:
                    //TickString tickGeneric = (TickString)y;
                    quote.halted = Convert.ToInt32(y.Value);
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;

                case TickType.BID_OPTION:
                    //optionComp = (TickOptionComp)y;
                    quote.bidOptComp =
                        DoubleDecimalConverter.DoubleToDecimal(Convert.ToDouble(y.OptionPrice));
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;
                case TickType.ASK_OPTION:
                    //optionComp = (TickOptionComp)y;
                    quote.askOptComp =
                        DoubleDecimalConverter.DoubleToDecimal(Convert.ToDouble(y.OptionPrice));
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;
                case TickType.LAST_OPTION:
                    //optionComp = (TickOptionComp)y;
                    quote.lastOptComp =
                        DoubleDecimalConverter.DoubleToDecimal(Convert.ToDouble(y.OptionPrice));
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;
                case TickType.MODEL_OPTION:
                    //optionComp = (TickOptionComp)y;
                    quote.modelOptComp =
                        DoubleDecimalConverter.DoubleToDecimal(Convert.ToDouble(y.OptionPrice));
                    quote.LocalTransactionTime = y.LocalTransactionTime;
                    quote.ConId = y.ConId;
                    break;
            }
            #endregion
            quotelist.Add(quote);
            if (Counter % 100 == 0)
            {
                //IO.SerializeJson(quotelist, "quotes.json", true);
                DateTime endtime = DateTime.Now;
                TimeSpan span = endtime - start;
                Console.WriteLine($"{Counter} {span}");
                start = DateTime.Now;
            }
            Counter++;
            return json;
        }
        public void UpdateRedis(string message)
        {
            ContractQuote quote;
            //TickPrice tickPrice;
            //TickSize tickSize;
            //TickOptionComp toc;

            KeyValuePair<int, object> kvp;
            try
            {
                kvp = JsonConvert.DeserializeObject<KeyValuePair<int, object>>(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            dynamic y = kvp.Value;
            int tickType = y.TickType;
            //long rediskey = Convert.ToInt64(y.ConId);
            //quote = GetQuoteFromDictionary(rediskey, System.Convert.ToInt64(y.ConId));
            string rediskey = $"contractquote:{y.ConId}";
            quote = Getter.Get(Convert.ToInt64(y.ConId.ToString()));
            if (quote == null)
                throw new NullReferenceException("The quote is null");

            #region "TickType"
            switch (tickType)
            {
                case TickType.BID:
                    //tickPrice = TickPriceFactory.Create(y);
                    quote.bidPrice = y.Price;
                    break;
                case TickType.ASK:
                    //tickPrice = TickPriceFactory.Create(y);
                    quote.askPrice = y.Price;
                    break;
                case TickType.LAST:
                    //tickPrice = TickPriceFactory.Create(y);
                    quote.lastPrice = y.Price;
                    break;
                case TickType.HIGH:
                    //tickPrice = TickPriceFactory.Create(y);
                    quote.high = y.Price;
                    break;
                case TickType.LOW:
                    //tickPrice = TickPriceFactory.Create(y);
                    quote.low = y.Price;
                    break;
                case TickType.CLOSE:
                    //tickPrice = TickPriceFactory.Create(y);
                    quote.close = y.Price;
                    break;
                case TickType.OPEN:
                    //tickPrice = TickPriceFactory.Create(y);
                    quote.open = y.Price;
                    break;

                case TickType.BID_SIZE:
                    //tickSize = TickSizeFactory.Create(y);
                    quote.bidSize = y.Size;
                    break;

                case TickType.ASK_SIZE:
                    //tickSize = TickSizeFactory.Create(y);
                    quote.askSize = y.Size;
                    break;

                case TickType.LAST_SIZE:
                    //tickSize = TickSizeFactory.Create(y);
                    quote.lastSize = y.Size;
                    break;
                case TickType.VOLUME:
                    //tickSize = TickSizeFactory.Create(y);
                    quote.volume = y.Size;
                    break;


                case TickType.LAST_TIMESTAMP:
                    TickString tickString = TickStringFactory.Create(y);
                    //var ts = System.Convert.ToInt64(y.LastTimestamp);
                    quote.lastTimestamp = Convert.ToInt64(y.LastTimestamp);
                    break;

                case TickType.HALTED:
                    //var tg = TickGenericFactory.Create(y);
                    quote.halted = System.Convert.ToInt32(y.Value);
                    break;

                case TickType.BID_OPTION:
                    //toc = TickOptionCompFactory.Create(y);
                    quote.bidOptComp = DoubleDecimalConverter.DoubleToDecimal(Convert.ToDouble(y.OptionPrice));
                    break;
                case TickType.ASK_OPTION:
                    //toc = TickOptionCompFactory.Create(y);
                    quote.askOptComp = DoubleDecimalConverter.DoubleToDecimal(Convert.ToDouble(y.OptionPrice));
                    break;
                case TickType.LAST_OPTION:
                    //toc = TickOptionCompFactory.Create(y);
                    quote.lastOptComp = DoubleDecimalConverter.DoubleToDecimal(Convert.ToDouble(y.OptionPrice));
                    break;
                case TickType.MODEL_OPTION:
                    //toc = TickOptionCompFactory.Create(y);
                    quote.modelOptComp = DoubleDecimalConverter.DoubleToDecimal(Convert.ToDouble(y.OptionPrice));
                    break;
                default:
                    throw new Exception("Invalid TickType");
            }
            #endregion

            string serialized = CsvSerializer.Serialize(quote);
            quote.Id = y.Id;
            quote.lastTimestamp = y.LastTimestamp;
            //History.Add(quote);
            quotes[rediskey] = quote;
            Db.KeyDelete(rediskey);
            _cacheClient.Replace(rediskey, quote);
            WriteLine(serialized);

        }

    }
}

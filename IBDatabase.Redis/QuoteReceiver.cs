using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using IBApi;
using IBDatabase.Factory;
using IBDatabase.Models;
using IBUtility;
using Newtonsoft.Json;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;
using TWSLib;
using static System.Console;

namespace IBDatabase.Redis
{
    public class QuoteReceiver : IReciever<ContractQuote>
    {
        //private const string Connectionstring = "bizcad-ibredis-azure.westus.cloudapp.azure.com,abortConnect=false";
        private const string Connectionstring = "localhost";
        //private readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(Connectionstring);
        

        public IDatabase Db { get; set; }
        public ISubscriber Sub { get; set; }
        public int Counter;
        //public List<string> MessageList;

        private readonly StackExchangeRedisCacheClient _cacheClient;
        public QuoteGetter Getter;
        DBContractsConnectedRepository _repo = new DBContractsConnectedRepository();
        private Dictionary<string, ContractQuote> quotes;

        public BufferBlock<string> queue;
        public ContractQuoteConsumer Consumer;

        public List<ContractQuote> History;
        public QuoteReceiver()
        {
            Db = RedisConnectorHelper.Connection.GetDatabase();
            Sub = RedisConnectorHelper.Connection.GetSubscriber();

            quotes = new Dictionary<string, ContractQuote>();

            var serializer = new NewtonsoftSerializer();
            _cacheClient = new StackExchangeRedisCacheClient(serializer);
            Getter = new QuoteGetter(new ContractQuoteConnectedRepository(), _repo);
            var contractGetter = new ContractGetter(_repo);
           
            //List<Contract> list = new List<Contract>();
            List<DBContract> dblist = contractGetter.GetList();
            foreach (DBContract item in dblist)
            {
                ContractQuote quote = ContractQuoteFactory.Create(item);
                string rediskey = $"contractquote:{item.ConId}";
                quotes.Add(rediskey, quote);
            }
        }

        public void Init()
        {
            queue = new BufferBlock<string>();
            Consumer = new ContractQuoteConsumer();
        }



        /// <summary>
        /// Classic Subscribe
        /// </summary>
        public void Subscribe()
        {

            Sub.Subscribe("messages", (channel, message) =>
            {
                if (Sub.IsConnected("messages"))
                {
                    UpdateRedis(message);

                    if (Counter % 1000 == 0)
                    {
                        Debug.WriteLine($"{Counter} - {DateTime.Now}");
                        //IO.SerializeJson(quotes,$"quotes.json",true);
                        //Getter.SaveCsv(quotes, $"quotes{Counter/1000}.csv");
                    }
                }
            });

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
            string rediskey = $"contractquote:{y.ConId}" ;
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
        public void UpdateRedis1(string message)
        {
            ContractQuote quote;
            //TickPrice tickPrice;
            //TickSize tickSize;
            //TickOptionComp toc;



            //WriteLine(message);
            //string rediskey = "contractquote:";
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
            int tickType = kvp.Key;
            string rediskey = $"contractquote:{y.ConId}";
            quote = GetQuoteFromDictionary(rediskey, System.Convert.ToInt64(y.ConId));
            //quote = _getter.Get(System.Convert.ToInt64(y.ConId.ToString()));
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
            History.Add(quote);
            quotes[rediskey] = quote;
            //_cacheClient.Replace(rediskey, quote);

        }

        private ContractQuote GetQuoteFromDictionary(string key, Int64 conId)
        {
            if (!quotes.ContainsKey(key))
                quotes.Add(key, Getter.Get(conId));
            return quotes[key];
        }
        #region async_not_used
        //private async Task<bool> UpdateRedisAsync(string message)
        //{
        //    ContractQuote quote;
        //    TickPrice tickPrice;
        //    TickSize tickSize;
        //    TickOptionComp toc;

        //    string rediskey = "contractquote:";
        //    KeyValuePair<int, object> kvp;
        //    try
        //    {
        //        kvp = JsonConvert.DeserializeObject<KeyValuePair<int, object>>(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    dynamic y = kvp.Value;
        //    int tickType = y.TickType;
        //    rediskey += y.ConId.ToString();

        //    if (_cacheClient.Exists(rediskey))
        //    {
        //        quote = _cacheClient.GetAsync<ContractQuote>(rediskey).Result;
        //    }
        //    else
        //    {
        //        quote = new ContractQuote { ConId = y.ConId };
        //        quote = _cacheClient.AddAsync<ContractQuote>(y.ConId, quote).Result;

        //    }

        //    #region "TickType"
        //    switch (tickType)
        //    {
        //        case TickType.BID:
        //            tickPrice = TickPriceFactory.Create(y);
        //            quote.bidPrice = tickPrice.Price;
        //            break;
        //        case TickType.ASK:
        //            tickPrice = TickPriceFactory.Create(y);
        //            quote.askPrice = tickPrice.Price;
        //            break;
        //        case TickType.LAST:
        //            tickPrice = TickPriceFactory.Create(y);
        //            quote.lastPrice = tickPrice.Price;
        //            break;
        //        case TickType.HIGH:
        //            tickPrice = TickPriceFactory.Create(y);
        //            quote.high = tickPrice.Price;
        //            break;
        //        case TickType.LOW:
        //            tickPrice = TickPriceFactory.Create(y);
        //            quote.low = tickPrice.Price;
        //            break;
        //        case TickType.CLOSE:
        //            tickPrice = TickPriceFactory.Create(y);
        //            quote.close = tickPrice.Price;
        //            break;
        //        case TickType.OPEN:
        //            tickPrice = TickPriceFactory.Create(y);
        //            quote.open = tickPrice.Price;
        //            break;

        //        case TickType.BID_SIZE:
        //            tickSize = TickSizeFactory.Create(y);
        //            quote.bidSize = tickSize.Size;
        //            break;

        //        case TickType.ASK_SIZE:
        //            tickSize = TickSizeFactory.Create(y);
        //            quote.askSize = tickSize.Size;
        //            break;

        //        case TickType.LAST_SIZE:
        //            tickSize = TickSizeFactory.Create(y);
        //            quote.lastSize = tickSize.Size;
        //            break;
        //        case TickType.VOLUME:
        //            tickSize = TickSizeFactory.Create(y);
        //            quote.volume = tickSize.Size;
        //            break;


        //        case TickType.LAST_TIMESTAMP:
        //            TickString tickString = TickStringFactory.Create(y);
        //            quote.lastTimestamp = TimeStampConverter.ToDateTime(tickString.LastTimestamp);
        //            break;

        //        case TickType.HALTED:
        //            var tg = TickGenericFactory.Create(y);
        //            quote.halted = System.Convert.ToInt32(tg.Value);
        //            break;

        //        case TickType.BID_OPTION:
        //            toc = TickOptionCompFactory.Create(y);
        //            quote.bidOptComp = DoubleDecimalConverter.DoubleToDecimal(toc.OptionPrice);
        //            break;
        //        case TickType.ASK_OPTION:
        //            toc = TickOptionCompFactory.Create(y);
        //            quote.askOptComp = DoubleDecimalConverter.DoubleToDecimal(toc.OptionPrice);
        //            break;
        //        case TickType.LAST_OPTION:
        //            toc = TickOptionCompFactory.Create(y);
        //            quote.lastOptComp = DoubleDecimalConverter.DoubleToDecimal(toc.OptionPrice);
        //            break;
        //        case TickType.MODEL_OPTION:
        //            toc = TickOptionCompFactory.Create(y);
        //            quote.modelOptComp = DoubleDecimalConverter.DoubleToDecimal(toc.OptionPrice);
        //            break;
        //        default:
        //            throw new Exception("Invalid TickType");
        //    }
        //    #endregion
        //    WriteLine(message);
        //    return await _cacheClient.ReplaceAsync(rediskey, quote);
        //}
        //public async Task SubscribeAsync()
        //{
        //    RedisChannel channel = new RedisChannel("messages", RedisChannel.PatternMode.Auto);
        //    Action<string> messageTarget;
        //    Func<string, Task> doit = UpdateRedisAsync(string message)
        //    try
        //    {
        //        await _cacheClient.SubscribeAsync<string>(channel, Debug.WriteLineAsync);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        #endregion

        public void Unsubscribe()
        {
            Sub.Unsubscribe("messages");
        }

        public void SaveCsv(string historyCsv)
        {
            throw new NotImplementedException();
        }
    }

}

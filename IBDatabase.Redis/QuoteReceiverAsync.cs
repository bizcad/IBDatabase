using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using IBApi;
using IBDatabase.Models;
using StackExchange.Redis;
using IBUtility;

namespace IBDatabase.Redis
{
    public class QuoteReceiverAsync : IReceiverAsync<ContractQuote>
    {
        public BufferBlock<string> Queue;
        public ContractQuoteConsumer Consumer;
        private RedisChannel chan = new RedisChannel("messages", RedisChannel.PatternMode.Auto);

        public QuoteReceiverAsync()
        {
            Queue = new BufferBlock<string>();
            Consumer = new ContractQuoteConsumer();
        }

        /// <summary>
        /// Creates a subscription which forwards the message to a queue
        /// </summary>
        /// <returns></returns>
        public async Task SubscribeAsync()
        {
            var sub = RedisConnectorHelper.Connection.GetSubscriber();

            /* FALLING BACK TO THE ISubscriber works */
            await sub.SubscribeAsync(chan, async (channel, message) =>
            {
                if (sub.IsConnected("messages"))
                {
                    await Queue.SendAsync<string>(message);
                    await Consumer.ConsumeAsync(Queue);
                }
                else
                {
                    throw new Exception("Not connected");
                }
            });
        }

        public void Unsubscribe()
        {
            RedisConnectorHelper.Connection.GetSubscriber().UnsubscribeAsync(chan);
        }

        //public async Task FeedFromRedisAsync(string whereSerialized = "Contract.json")
        //{
        //    List<Contract> contracts = IO.GetContractList(whereSerialized);
        //    var db = RedisConnectorHelper.Connection.GetDatabase();
        //    foreach (var contract in contracts)
        //    {
        //        var value = db.StringGet("contractquote:" + contract.ConId).ToString();

        //        var message = value;
        //        await Queue.SendAsync<string>(message);
        //        await Consumer.ConsumeAsync(Queue);

        //    }
        //}

        //public async Task FeedFromFileAsync(string whereSerialized = "Objects.json")
        //{
        //    List<string> contracts = IO.GetContractList("Contract.json");
        //    foreach (var contract in contracts)
        //    {

        //    }
        //}
        public void FeedFromRedisAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}

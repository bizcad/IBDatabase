using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace IBDatabase
{
    public class ContractQuoteManager
    {
        
        public BufferBlock<string> queue;
        public ContractQuoteConsumer c;

        public ContractQuoteManager()
        {
            
            queue = new BufferBlock<string>();
            c = new ContractQuoteConsumer();
        }
        public async Task SimpleQueue()
        {
            DateTime start = DateTime.Now;
            // Define the mesh.
            queue = new BufferBlock<string>();

            // Start the producer and consumer.
            var consumer = c.ConsumeAsync(queue);

            // Wait for everything to complete.
            await Task.WhenAll(consumer, queue.Completion);


            //var results = await consumer;
            DateTime endtime = DateTime.Now;
            TimeSpan span = endtime - start;
            Console.WriteLine($"Time {span}");
            //var x = results.ToList();
            //Console.WriteLine($"Length of result = {x.Count} {span}");


        }

    }
}

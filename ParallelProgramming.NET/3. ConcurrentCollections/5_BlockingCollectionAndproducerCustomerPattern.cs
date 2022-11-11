using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._3._ConcurrentCollections
{
    internal class _5_BlockingCollectionAndproducerCustomerPattern
    {
        static BlockingCollection<int> messages = new BlockingCollection<int>(
          new ConcurrentBag<int>(), 10 /* bounded */
        );

        static CancellationTokenSource cts = new CancellationTokenSource();

        public static void ProduceAndConsume()
        {
            var producer = Task.Factory.StartNew(RunProducer);
            var consumer = Task.Factory.StartNew(RunConsumer);

            try
            {
                Task.WaitAll(new[] { producer, consumer }, cts.Token);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }

        private static Random random = new Random();

        private static void RunConsumer()
        {
            foreach (var item in messages.GetConsumingEnumerable())
            {
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"-{item} ::      {messages.Count}");
                Thread.Sleep(random.Next(2000));
            }
        }

        private static void RunProducer()
        {

            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                int i = random.Next(100);
                messages.Add(i);
                //try
                //{
                //    messages.Add(i);
                //}
                //catch (Exception)
                //{
                //    Console.WriteLine("Has exceeded the boundary for BlockingColleciton.");
                //}
                Console.WriteLine($"+{i}\t");
                Thread.Sleep(random.Next(1000));
            }
        }

        public static void Drive()
        {
            Task.Factory.StartNew(ProduceAndConsume, cts.Token);

            Console.ReadKey();
            cts.Cancel();
        }

    }
}

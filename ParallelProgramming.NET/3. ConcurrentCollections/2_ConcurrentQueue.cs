using System;
using System.Collections.Concurrent;

namespace ParallelProgramming.NET._3._ConcurrentCollections
{
    internal class _2_ConcurrentQueue
    {

        public static void Drive()
        {
            var q = new ConcurrentQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);

            // Queue: 2 1 <- front

            int result;

            //int last = q.Dequeue();
            if (q.TryDequeue(out result))
            {
                Console.WriteLine($"Removed element {result}");
            }

            // Queue: 2

            //int peeked = q.Peek();
            if (q.TryPeek(out result))
            {
                Console.WriteLine($"Last element is {result}");
            }
        }

    }
}

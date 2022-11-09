using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._3._ConcurrentCollections
{
    internal class _3_ConcurrentStack
    {

        public static void Drive()
        {
            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                int i1 = i;
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    bag.Add(i1);
                    Console.WriteLine($"{Task.CurrentId} has added {i1}");
                    int result;
                    if (bag.TryPeek(out result))
                    {
                        Console.WriteLine($"{Task.CurrentId} has peeked the value {result}");
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            int last;
            if (bag.TryTake(out last))
            {
                Console.WriteLine($"I go {last}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._6._ParallelLINQ
{
    internal class _4_CustomAggregation
    {

        public static void Drive()
        {
            var sum = Enumerable.Range(1, 1000).Sum();
            Console.WriteLine($"Sum: {sum}");

            var s = 0;
            Enumerable.Range(1, 1000)
                .AsParallel()
                .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                .ForAll(x =>
                {
                    s += x;
                });

            Thread.Sleep(5000);
            Console.WriteLine($"Result 1: {s}");
            Console.WriteLine($"Result 2: {(1 + 1000) * 1000 / 2}");

            var sumAggregated = Enumerable.Range(1, 1000)
                .Aggregate(0, (i, acc) => i + acc); Console.WriteLine($"Sum aggreagted: {sumAggregated}");

            var sumParallel = ParallelEnumerable.Range(1, 1000)
                .Aggregate(0, (partialSum, i) => partialSum += i, (total, subTotal) => total += subTotal, i => i);
            Console.WriteLine($"Sum aggreagted: {sumParallel}");

        }
    }
}

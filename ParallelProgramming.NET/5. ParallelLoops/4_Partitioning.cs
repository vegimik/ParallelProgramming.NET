using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._5._ParallelLoops
{
    public class _4_Partitioning
    {

        [Params(5000, 10000, 15000)]
        public int Partitionare { get; set; }

        [Benchmark]
        public void SquareEachValue()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            Parallel.ForEach(values, x => { results[x] = (int)Math.Pow(x, 2); });
        }

        [Benchmark]
        public void SquareEachValueChunked()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            //var part = Partitioner.Create(0, count, 10000); // rangeSize = size of each subrange
            var part = Partitioner.Create(0, count, Partitionare); // rangeSize = size of each subrange

            Parallel.ForEach(part, range =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    results[i] = (int)Math.Pow(i, 2);
                }
            });
        }


        public static void Drive()
        {
            //var obj = new _4_Partitioning();
            //obj.SquareEachValue();
            //obj.SquareEachValueChunked();
            var summary = BenchmarkRunner.Run<_4_Partitioning>();
            Console.WriteLine(summary);
        }
    }
}

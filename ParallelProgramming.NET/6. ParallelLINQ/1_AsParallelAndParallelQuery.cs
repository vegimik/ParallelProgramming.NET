using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Linq;

namespace ParallelProgramming.NET._6._ParallelLINQ
{
    public class _1_AsParallelAndParallelQuery
    {
        const int count = 50;

        [Benchmark]
        public void AsParallelDriver()
        {
            var items = Enumerable.Range(1, count).ToArray();
            var results = new int[count];

            items.AsParallel().ForAll(x =>
            {
                int newvalue = (int)Math.Pow(x, 3);
                //Console.WriteLine($"{x}^3 = {newvalue} ({Task.CurrentId})\t");
                results[x - 1] = newvalue;
            });

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine("Result from array:\n");
            //foreach (var item in results)
            //    Console.WriteLine($"{item}\t");
            //Console.WriteLine();
        }

        [Benchmark]
        public void AsParallelOrderedDriver()
        {
            var items = Enumerable.Range(1, count).ToArray();
            var cubes = items.AsParallel().AsOrdered().Select(x => Math.Pow(x, 3));

            //Console.WriteLine();
            //Console.WriteLine("Result from array ordered:\n");
            //foreach (var item in cubes)
            //    Console.WriteLine($"{item}\t");
            //Console.WriteLine();
        }



        #region Benchmark Result
        //|                  Method |        Mean |     Error |    StdDev |      Median |
        //|------------------------ |------------:|----------:|----------:|------------:|
        //|        AsParallelDriver | 15,062.6 ns | 289.87 ns | 768.70 ns | 14,760.1 ns |
        //| AsParallelOrderedDriver |    378.7 ns |   7.29 ns |   8.68 ns |    380.6 ns |
        #endregion End Benchmark Result
        public static void Drive()
        {
            //AsParallelDriver();
            //AsParallelOrderedDriver();

            var summary = BenchmarkRunner.Run<_1_AsParallelAndParallelQuery>();
            Console.WriteLine(summary);



        }
    }
}

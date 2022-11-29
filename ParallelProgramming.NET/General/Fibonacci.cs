using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.NET.General
{
    internal class Fibonacci
    {

        public static void Drive()
        {
            Console.WriteLine($"First result: {FibonacciSequential(10)}");
            Console.WriteLine($"Second result: {FibonacciInvoke(10)}");
            Console.WriteLine($"Third result: {FibonacciTask(10)}");
        }

        static long FibonacciSequential(long n)
        {
            if (n < 2)
            {
                return n;
            }

            var one = FibonacciSequential(n - 1);
            var two = FibonacciSequential(n - 2);

            return one + two;
        }
        static long FibonacciInvoke(long n)
        {
            if (n < 2)
            {
                return n;
            }

            long one = default(long);
            long two = default(long);

            Parallel.Invoke(
                () => one = FibonacciInvoke(n - 1),
                () => two = FibonacciInvoke(n - 2));

            return one + two;
        }
        static long FibonacciTask(long n)
        {
            if (n < 2)
            {
                return n;
            }

            var two = new Task<long>(() => FibonacciTask(n - 2));
            two.Start();
            var one = FibonacciTask(n - 1);
            Task.WaitAll(two);

            return one + two.Result;
        }
    }

}

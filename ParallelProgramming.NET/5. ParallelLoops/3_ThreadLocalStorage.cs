using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._5._ParallelLoops
{
    internal class _3_ThreadLocalStorage
    {
        public static ParallelLoopResult result;
        public static void Drive()
        {
            int sum = 0;


            var task1 = Task.Factory.StartNew(() =>
            {
                result = Parallel.For(1, 1001, (i) =>
                {
                    Interlocked.Add(ref sum, i);
                });
            });

            var task2 = task1.ContinueWith((t) =>
            {
                Console.WriteLine($"The result after task: {t} is r={sum} ");
            });
            task2.Wait();

            Console.WriteLine($"Sum of numbers in range [1, 1001] is ={(1 + 1000) * 1000 / 2.0}");


            var sumModified = 0;
            Parallel.For(1, 1001, () => 0, (x, state, tls) =>
            {
                tls += x;
                return tls;
            },
            partialSum =>
            {
                Interlocked.Add(ref sumModified, partialSum);
            });



        }
    }
}

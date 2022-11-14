using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._5._ParallelLoops
{
    internal class _2_ParallelLoopBreakingCancellationException
    {
        public static ParallelLoopResult result;
        public static void Drive()
        {
            var cts = new CancellationTokenSource();

            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cts.Token;
            try
            {
                result = Parallel.For(0, 20, po, (int x, ParallelLoopState state) =>
                {
                    po.CancellationToken.ThrowIfCancellationRequested();
                    Console.WriteLine($"{x}: [{Task.CurrentId}]");
                    if (x == 10)
                    {
                        //state.Stop();
                        //state.Break();
                        //throw new Exception();

                        cts.Cancel();
                    }
                });


            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine($"Exception: {e.Message}");
                    return true;
                });
            }
            catch (OperationCanceledException oce)
            {
                Console.WriteLine($"Exception: {oce.Message}");
            }

            Console.WriteLine($"Result is {result}, status: {result.IsCompleted}");
            if (result.LowestBreakIteration.HasValue)
                Console.WriteLine($"The lowest break iteration is {result.LowestBreakIteration}");
        }
    }
}

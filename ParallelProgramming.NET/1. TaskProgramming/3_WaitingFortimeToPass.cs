using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._1._TaskProgramming
{
    internal class _3_WaitingFortimeToPass
    {
        public static void Drive()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            token.Register(() =>
            {
                Console.WriteLine("Cancellation has been requested.");
            });
            var t = new Task(() =>
            {
                Console.WriteLine("Press any key to disarm, you have 5 seconds");
                bool cancelled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(cancelled ? "bomb disarmed." : "BOOM!!!");
            }, token);
            t.Start();

            Console.ReadKey();
            cts.Cancel();


            Console.WriteLine($"Main program done.");
            Console.ReadKey();
        }
    }
}

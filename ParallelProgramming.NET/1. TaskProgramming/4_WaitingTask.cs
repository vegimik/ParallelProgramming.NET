using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._1._TaskProgramming
{
    internal class _4_WaitingTask
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
                Console.WriteLine("I take 5 seconds");
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"we are runing to the thread: {Thread.CurrentThread.ManagedThreadId} or [Task: {Task.CurrentId}]");
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("I'm done");
            }, token);
            t.Start();

            Task t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);

            //Task.WaitAny(t, t2);
            Task.WaitAny(new[] { t, t2 }, 4000, token);
            Console.WriteLine($"Task t status is {t.Status}");
            Console.WriteLine($"Task t status is {t2.Status}");

            Console.WriteLine($"Main program done.");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._4._TaskCoordination
{
    internal class _4_CountdownEvent
    {
        private static int taskCount = 5;
        static CountdownEvent cte = new CountdownEvent(taskCount);
        private static Random random = new Random();

        public static void Drive()
        {
            for (int i = 0; i < taskCount; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");
                    Thread.Sleep(random.Next(3000));
                    cte.Signal();
                    Console.WriteLine($"Exiting task {Task.CurrentId}");
                });
            }

            var finaltask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Waiting for othr tasks to complete in {Task.CurrentId}");
                cte.Wait();
                Console.WriteLine($"All tasks completed");
            });
            finaltask.Wait();

        }
    }
}

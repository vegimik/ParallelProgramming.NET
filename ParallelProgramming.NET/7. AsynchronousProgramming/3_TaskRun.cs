using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._7._AsynchronousProgramming
{
    internal class _3_TaskRun
    {

        public static void Drive()
        {
            var task = Task.Factory.StartNew(async () =>
            {
                await Task.Delay(5000);
                return 123;
            }).Unwrap();

            Console.ReadKey();
            Console.WriteLine($"Result of task: {task}");
            Console.WriteLine($"Result value of task: {task.Result}");
        }
    }
}

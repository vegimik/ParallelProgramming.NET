using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._1._TaskProgramming
{
    internal class _5_TaskThrowEception
    {
        public static void Drive()
        {
            var t = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Can't do this") { Source = "t" };
            });
            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't do this") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(new[] { t, t2 }, 3000);
            }
            catch (AggregateException ae)
            {
                foreach (var itemException in ae.InnerExceptions)
                {
                    Console.WriteLine($"Exception {itemException.GetType()} from {itemException.Source}");
                }

                ae.Handle(e =>
                {
                    if (e is InvalidOperationException)
                    {
                        Console.WriteLine("Invalid op!");
                        return true;
                    }
                    return false;
                });
            }
            Console.WriteLine($"Main program done.");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._4._TaskCoordination
{
    /// <summary>
    /// ManualResetEventSlim and AutoResetEvent
    /// </summary>
    internal class _5_ManualResetEvenSlimAndAutoResetEvent
    {
        public static void Drive()
        {
            ManualResetEventSlimDrive();
            Thread.Sleep(3000);
            AutoResetEventDrive();

        }

        static void ManualResetEventSlimDrive()
        {
            var evt = new ManualResetEventSlim();
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                evt.Set();
            });
            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water ...");
                evt.Wait();
                Console.WriteLine("here is your tea");
            });
            makeTea.Wait();

        }
        static void AutoResetEventDrive()
        {
            var evt = new AutoResetEvent(false);
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                evt.Set();
                //evt.Set(); // waitone at 2
            });
            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water ...");
                evt.WaitOne();
                Console.WriteLine("here is your tea");
                var ok = evt.WaitOne(1000);
                if (ok)
                {
                    Console.WriteLine("Enjoy your tea");
                }
                else
                {
                    Console.WriteLine("No tea for you");
                }
            });
            makeTea.Wait();

        }
    }
}

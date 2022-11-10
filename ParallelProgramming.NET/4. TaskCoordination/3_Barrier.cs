using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._4._TaskCoordination
{
    internal class _3_Barrier
    {
        static Barrier barrier = new Barrier(2, b =>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished.");
            Console.WriteLine($"Barrier status is: total number of thread that are arrived are {b.CurrentPhaseNumber}, there are left {b.ParticipantsRemaining}, and the result flag is {b.ParticipantCount == b.CurrentPhaseNumber}");
            //b.ParticipantCount
            //b.ParticipantsRemaining

            // add/remove participants
        });

        public static void Water()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Putting the kettle on (takes a bit longer).");
            Thread.Sleep(2000);
            barrier.SignalAndWait(); // signaling and waiting fused
            Console.WriteLine("Putting water into cup.");
            barrier.SignalAndWait();
            Console.WriteLine("Putting the kettle away.");

        }

        public static void Cup()
        {
            Console.WriteLine("Finding the nicest tea cup (only takes a second).");
            barrier.SignalAndWait();
            Console.WriteLine("Adding tea.");
            barrier.SignalAndWait();
            Console.WriteLine("Adding sugar");
        }

        public static void Drive()
        {
            var cup = Task.Factory.StartNew(Cup);
            var water = Task.Factory.StartNew(Water);

            var tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, tasks =>
            {
                Console.WriteLine("Enjoy your cup of tea.");
            });

            tea.Wait();
        }

    }
}

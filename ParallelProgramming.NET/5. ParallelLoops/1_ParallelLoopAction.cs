using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._5._ParallelLoops
{
    internal class _1_ParallelLoopAction
    {
        public static IEnumerable<int> Range(int start, int end, int step)
        {
            for (int i = start; i < end; i += step)
            {
                yield return i;
            }
        }
        public static void Drive()
        {
            var a = new Action(() => Console.WriteLine($"First: {Task.CurrentId}"));
            var b = new Action(() => Console.WriteLine($"Second: {Task.CurrentId}"));
            var c = new Action(() => Console.WriteLine($"Third: {Task.CurrentId}"));

            var arrayTasks = new List<Action>();
            for (int i = 0; i < 10; i++)
            {
                int j = i;
                arrayTasks.Add(new Action(() => Console.WriteLine($"{j}. Thread: {Task.CurrentId}")));
            }


            Parallel.Invoke(arrayTasks.ToArray());

            Parallel.For(1, 11, (i) =>
            {
                Console.WriteLine($"{i}^2 = {i * i} \t");
            });

            var words = new string[] { "oh", "what", "a", "night" };
            Parallel.ForEach(words, (word) =>
            {
                Console.WriteLine($"{word} has length {word.Length} (task {Task.CurrentId})");
            });

            Parallel.ForEach(Range(1, 20, 3), Console.WriteLine);
        }

        public static void ThreadPoolDrive()
        {
            ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;
            int indx = 0;
            foreach (ProcessThread thread in currentThreads)
            {
                Console.WriteLine($"{indx++}. Current thread is ID:{thread.Id}, PriorityLevel{thread.PriorityLevel}, UserProcessorTime{thread.UserProcessorTime}");
            }
        }
    }
}


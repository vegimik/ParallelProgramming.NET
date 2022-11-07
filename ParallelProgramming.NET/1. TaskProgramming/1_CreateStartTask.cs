using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._1._TaskProgramming
{
    internal class _1_CreateStartTask
    {
        public static void Write(char c)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.WriteLine($"{c}({i})");
            }

        }

        public static void Write(object o)
        {

            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"\nTask with id={Task.CurrentId} preocessing object {o} ...");
            return o.ToString().Length;
        }

        public static void Drive()
        {
            //Task.Factory.StartNew(() => Write('.'));

            //var t = new Task(() => Write('?'));
            //t.Start();

            //Task t = new Task(Write, "hello");
            //t.Start();
            //Task.Factory.StartNew(Write, 123);

            string text1 = "testing", text2 = "this";
            var task1 = new Task<int>(TextLength, text1);
            task1.Start();

            Task<int> task2 = Task.Factory.StartNew<int>(TextLength, text2);

            Console.WriteLine($"Length of {text1} is {task1.Result}");
            Console.WriteLine($"Length of {text2} is {task2.Result}");

            Console.Write('-');

        }
    }
}

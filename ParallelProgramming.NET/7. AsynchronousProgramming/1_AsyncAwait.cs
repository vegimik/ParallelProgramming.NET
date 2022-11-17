using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._7._AsynchronousProgramming
{
    internal class _1_AsyncAwait
    {
        public int CalculateValue()
        {
            Thread.Sleep(5000);
            return 123;
        }


        public async Task<int> CalculateValueAsync()
        {
            //return Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(5000);
            //    return 123;
            //});

            await Task.Delay(5000);
            return 123;
        }

        public async void Drive()
        {
            var value = await CalculateValueAsync();
            Console.WriteLine($"Aync value calcualted is {value}");

            await Task.Delay(5000);
            using (var wc = new WebClient())
            {
                string data = await wc.DownloadStringTaskAsync("http://google.com/robots.txt");
                Console.WriteLine($"{data.Trim()}");

            }
        }

        public void DriveBuilder()
        {

            var task = Task.Factory.StartNew(() =>
            {
                var obj = new _1_AsyncAwait();
                obj.Drive();
            });

            Console.ReadKey();

            task.Wait();
        }
    }
}

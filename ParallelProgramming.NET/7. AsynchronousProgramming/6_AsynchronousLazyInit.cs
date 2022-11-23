using BenchmarkDotNet.Attributes;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._7._AsynchronousProgramming
{
    internal class _6_AsynchronousLazyInit
    {
        public class Staff
        {
            private static int value;
            private readonly Lazy<Task<int>> AutoIncValue = new Lazy<Task<int>>(
                 async () =>
                {
                    await Task.Delay(1000).ConfigureAwait(false);
                    return value++;
                });

            private readonly Lazy<Task<int>> AutoIncValue2 = new Lazy<Task<int>>(() => Task.Run(async () =>
            {
                await Task.Delay(1000);
                return value++;
            }));

            private AsyncLazy<Task<int>> AutoIncValue3 = new AsyncLazy<Task<int>>(
                async () =>
                {
                    await Task.Delay(1000);
                    return Task.FromResult(value++);
                });

            public async Task<int> getValueAsync() => await AutoIncValue.Value;
            public async Task UseValue()
            {
                int value = await AutoIncValue.Value;
            }

        }

        public static void Drive()
        {

        }
    }
}

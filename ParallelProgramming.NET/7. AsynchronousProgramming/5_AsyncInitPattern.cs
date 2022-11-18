using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._7._AsynchronousProgramming
{
    internal class _5_AsyncInitPattern
    {
        public interface IAsyncInit
        {
            Task InitTask { get; }

        }

        public class MyClass : IAsyncInit
        {
            public MyClass()
            {
                InitTask = InitAsync();
            }
            public Task InitTask { get; }

            public async Task InitAsync()
            {
                await Task.Delay(1000);
            }

        }

        public class MyOtherClass : IAsyncInit
        {
            private readonly MyClass myClass;
            public MyOtherClass(MyClass myClass)
            {
                this.myClass = myClass;
                InitTask = InitAsync();
            }
            public Task InitTask { get; }

            public async Task InitAsync()
            {
                if (myClass is IAsyncInit ai)
                {
                    await ai.InitTask;
                }
                await Task.Delay(1000);
            }

        }

        public static async void Drive()
        {
            var myClass = new MyClass();
            var myOtherClass = new MyOtherClass(myClass);

            await myOtherClass.InitTask;
        }
    }
}

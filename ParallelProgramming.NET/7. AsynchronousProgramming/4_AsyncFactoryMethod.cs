using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._7._AsynchronousProgramming
{
    internal class _4_AsyncFactoryMethod
    {
        public class Foo
        {
            public Foo()
            {

            }
            public async Task<Foo> InitAsync()
            {
                await Task.Delay(1000);
                //return Task.FromResult(new Foo());//.Result;
                return this;
            }
            public static Task<Foo> CreateAsync()
            {
                var result = new Foo();
                return result.InitAsync();
            }
        }

        public static async void Drive()
        {
            var foo = new Foo();
            await foo.InitAsync();

            Console.WriteLine();

            Foo createdFoo = await Foo.CreateAsync();
            Console.WriteLine($"Foo: {createdFoo}");

        }
    }
}

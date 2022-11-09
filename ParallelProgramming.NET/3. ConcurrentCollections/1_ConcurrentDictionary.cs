﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._3._ConcurrentCollections
{
    internal class _1_ConcurrentDictionary
    {
        private static ConcurrentDictionary<string, string> capitals
          = new ConcurrentDictionary<string, string>();

        public static void AddParis()
        {
            // there is no add, since you don't know if it will succeed
            bool success = capitals.TryAdd("France", "Paris");

            string who = Task.CurrentId.HasValue ? ("Task " + Task.CurrentId) : "Main thread";
            Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element.");
        }

        public static void Drive()
        {
            AddParis();
            Task.Factory.StartNew(AddParis).Wait();

            capitals["Russia"] = "Leningrad";
            var s = capitals.AddOrUpdate("Russia", "Moscow", (k, old) => old + " --> Moscow");
            Console.WriteLine("The capital of Russia is " + capitals["Russia"]);

            capitals["Sweden"] = "Uppsala";
            var capOfNorway = capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine($"The capital of Sweden is {capOfNorway}.");

            // removal
            const string toRemove = "Russia"; // make a mistake here
            string removed;
            var didRemove = capitals.TryRemove(toRemove, out removed);
            if (didRemove)
            {
                Console.WriteLine($"We just removed {removed}");
            }
            else
            {
                Console.WriteLine($"Failed to remove capital of {toRemove}");
            }

            // some operations are slow, e.g.,
            Console.WriteLine($"The ");

            foreach (var kv in capitals)
            {
                Console.WriteLine($" - {kv.Value} is the capital of {kv.Key}");
            }

            Console.ReadKey();
        }

    }
}

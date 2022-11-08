using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._2._DataSharingAndSynchronization
{
    internal class _4_Mutex_2
    { 
        public static void Drive()
        {
            const string appName = "_4_Mutex_Drive";
            Mutex mutex;
            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"Sorry, {appName} is already running");
            }
            catch (WaitHandleCannotBeOpenedException e)
            {
                Console.WriteLine($"We can run the program just fine");
                mutex = new Mutex(false, appName);
            }

            Console.ReadKey();
            mutex.ReleaseMutex();
        }

    }
}

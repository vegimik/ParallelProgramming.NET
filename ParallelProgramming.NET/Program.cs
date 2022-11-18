using ParallelProgramming.NET._1._TaskProgramming;
using ParallelProgramming.NET._2._DataSharingAndSynchronization;
using ParallelProgramming.NET._3._ConcurrentCollections;
using ParallelProgramming.NET._4._TaskCoordination;
using ParallelProgramming.NET._5._ParallelLoops;
using ParallelProgramming.NET._6._ParallelLINQ;
using ParallelProgramming.NET._7._AsynchronousProgramming;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            _5_AsyncInitPattern.Drive();

        }
    }
}

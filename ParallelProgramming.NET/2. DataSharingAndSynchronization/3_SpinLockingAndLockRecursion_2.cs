using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._2._DataSharingAndSynchronization
{
    internal class _3_SpinLockingAndLockRecursion_2
    {
        static SpinLock spl = new SpinLock(true);
        public static void LockRecursion(int x)
        {
            var lockTaken = false;
            try
            {
                spl.Enter(ref lockTaken);
            }
            catch (LockRecursionException lrc)
            {
                Console.WriteLine($"Expection: {lrc}");
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock, x = {x}");
                    LockRecursion(x - 1);
                    spl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take  a lock, x = {x}");
                }

            }
        }
        public static void Drive()
        {
            LockRecursion(5);
        }

    }
}

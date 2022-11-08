using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._2._DataSharingAndSynchronization
{
    internal class _5_ReadWriteLocks
    {

        public class BankAccount
        {
            private int balance;
            public int Balance
            {
                get
                {
                    return balance;
                }
                private set
                {
                    balance = value;
                }
            }

            public void Deposit(int amount)
            {
                balance += amount;
            }
            public void Withdraw(int amount)
            {
                balance -= amount;
            }

            public void Transfer(BankAccount where, int amount)
            {
                balance -= amount;
                where.Deposit(amount);
            }
        }

        static ReaderWriterLockSlim padLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        static Random random = new Random();
        public static void Drive()
        {
            int x = 0;
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    //padLock.EnterReadLock();
                    //padLock.EnterReadLock();
                    padLock.EnterUpgradeableReadLock();

                    if (i % 2 == 0)
                    {
                        padLock.EnterWriteLock();
                        x = 123;
                        padLock.ExitWriteLock();
                    }

                    Console.WriteLine($"Entered read lock, x = {x}");
                    Thread.Sleep(5000);

                    //padLock.ExitReadLock();
                    //padLock.ExitReadLock();
                    padLock.ExitUpgradeableReadLock();

                    Console.WriteLine($"Exit read lock, x = {x}.");
                }));

                try
                {
                    Task.WaitAll(tasks.ToArray());
                }
                catch (AggregateException ae)
                {
                    ae.Handle(e =>
                    {
                        Console.WriteLine(ae.Message);
                        return true;
                    });
                }

                while (true)
                {
                    Console.ReadKey();
                    padLock.EnterWriteLock();
                    Console.WriteLine("Write lock acquired");
                    int newvlaue = random.Next(10);
                    x = newvlaue;
                    Console.WriteLine($"Set x = {x}");
                    padLock.ExitWriteLock();
                    Console.WriteLine("Write lock released");
                }
            }

        }
    }
}

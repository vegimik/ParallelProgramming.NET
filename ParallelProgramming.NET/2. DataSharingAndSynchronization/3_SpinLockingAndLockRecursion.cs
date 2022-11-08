using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.NET._2._DataSharingAndSynchronization
{
    internal class _3_SpinLockingAndLockRecursion
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
                //Monitor.TryEnter();
                balance -= amount;
            }
        }

        public static void Drive()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            SpinLock spl = new SpinLock();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        var lockTaken = false;
                        try
                        {
                            spl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            if (lockTaken)
                            {
                                spl.Exit();
                            }
                        }
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        var lockTaken = false;
                        try
                        {
                            spl.Enter(ref lockTaken);
                            ba.Withdraw(100);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            if (lockTaken)
                            {
                                spl.Exit();
                            }
                        }
                    }
                }));

            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");
        }

    }
}

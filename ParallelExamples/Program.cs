using ParallelExamples.ThreadExamples;
using System;
using System.Threading;

namespace ParallelExamples
{
    class Program
    {

        static void Main(string[] args)
        {
            TryDeadLock();
        }

        static void TryDeadLock()
        {
            DeadLock dead = new DeadLock();
            var t1 = new Thread(() => dead.F1());
            var t2 = new Thread(() => dead.F2());
            t1.Name = "ciccio";
            t2.Name = "pippo";
            t1.Start();
            t2.Start();
        }



        static void TryLocking()
        {
            Account target = new Account(1000);

            Thread[] adders = new Thread[3];
            Thread[] removers = new Thread[3];

       

            for (int i = 0; i < adders.Length; i++)
            {
                adders[i] = new Thread(() =>
                {
                    for (int n = 0; n < 10000; n++)
                    {
                        target.Credit(100);
                    }
                });
            }
            for (int i = 0; i < removers.Length; i++)
            {
                removers[i] = new Thread(() =>
                {
                    for (int n = 0; n < 10000; n++)
                    {
                        target.Debit(100);
                    }
                });
            }

            for (int i = 0; i < adders.Length; i++)
            {
                adders[i].Start();
            }
            for (int i = 0; i < removers.Length; i++)
            {
                removers[i].Start();
            }

            for (int i = 0; i < adders.Length; i++)
            {
                adders[i].Join();
            }
            for (int i = 0; i < removers.Length; i++)
            {
                removers[i].Join();
            }

            Console.WriteLine($"Final balance is ... {target.Balance}");
        }
    }
}

using ParallelExamples.ThreadExamples;
using System;
using System.Threading;

namespace ParallelExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Account target = new Account(1000);

            Thread[] adders = new Thread[3];
            Thread[] removers = new Thread[3];
            for (int i = 0; i < adders.Length; i++)
            {
                adders[i] = new Thread(() =>
                {
                    for (int n = 0; n < 100; n++)
                    {
                        target.Credit(100);
                    }
                });
            }
            for (int i = 0; i < removers.Length; i++)
            {
                removers[i] = new Thread(() =>
                {
                    for (int n = 0; n < 100; n++)
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

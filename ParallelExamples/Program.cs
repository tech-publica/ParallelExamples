using ParallelExamples.ThreadExamples;
using System;
using System.Threading;

namespace ParallelExamples
{
    class Program
    {

        static void Main(string[] args)
        {
            ParameterizedThreadStart ps = (obj) => Console.WriteLine(obj);

            Thread t = new Thread((obj) => Console.WriteLine(obj));
            t.Start("ciao");
           
            t.Join();

            //TryDeadLock();
            TryWaitAndPulse();
        }

        static void TryWaitAndPulse()
        {
            Random r = new Random();
            Bagno b = new Bagno();
            var programmatoreJr = new Thread( () => 
            {
                while (true)
                {
                    Thread.Sleep(r.Next(5000));
                    if (b.AllNeededHaveBeenConsumed)
                    {
                        Console.WriteLine($"{Thread.CurrentThread.Name} says: Che giornata di M.. ma almeno e' finita!");
                        break;
                    }
                        
                    b.NonHoStudiatoAbbastanzaProgrammazione();
                }              
            });
            Thread[] programmatoriSenior = new Thread[Bagno.colors.Length];

          

            for (int i = 0; i < programmatoriSenior.Length; i++)
            {
                string color = Bagno.colors[i];
                int k = i;
                programmatoriSenior[i] = new Thread(() =>
                {                    
                    for (int j = 0; j < 2; j++)
                    {
                        Thread.Sleep(r.Next(2000));
                        b.Evacua(color);
                    }
                });
                programmatoriSenior[i].Name = "Programmatore Senior " + Bagno.colors[i];
            }
            


            programmatoreJr.Name = "Programmatore Jr.";
            for (int i = 0; i < programmatoriSenior.Length; i++)
            {
                programmatoriSenior[i].Start();
            }

            programmatoreJr.Start();
            

        }

        static void TryDeadLock()
        {
            DeadLock dead = new DeadLock();
            var t1 = new Thread(() => dead.F1());
            var t2 = new Thread(() => dead.F2());
            t1.Name = "T1";
            t2.Name = "T2";
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

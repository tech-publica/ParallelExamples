using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ParallelExamples.ThreadExamples
{
    public class DeadLock
    {
        private object o1 = new object();
        private object o2 = new object();
        public void F1()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.Name}  entered F1");
            lock(o1)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.Name}  got lock 1 going to sleep..");
                Thread.Sleep(2000);
                Console.WriteLine($"Thread {Thread.CurrentThread.Name}  woke up, calling G2..");
                G2();
            }
          
        }
        public void F2()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.Name}  entered F2");
            lock (o2)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.Name}  got lock 2 going to sleep..");
                Thread.Sleep(2000);
                Console.WriteLine($"Thread {Thread.CurrentThread.Name}  woke up, calling G1..");
                G1();
            }
        }
        public void G2()
        {
            lock(o2)
            {
                Console.WriteLine("G2, non ci arriveremo mai.. :(");
            }
        }
        public void G1()
        {
            lock (o1)
            {
                Console.WriteLine("G1, non ci arriveremo mai.. :(");
            }
        }

    }
}

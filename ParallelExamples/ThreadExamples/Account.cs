using System;
using System.Collections.Generic;
using System.Text;

namespace ParallelExamples.ThreadExamples
{
    public class Account
    {
        private readonly object balanceLock;
   

        private decimal balance;
        public decimal Balance { get => balance; }

        public Account(decimal initialBalance)
        {
            balance = initialBalance;
            balanceLock = new object();
        }


       
        public decimal Debit(decimal amount)
        {
            lock (balanceLock)
            {

                    Console.WriteLine($"Balance before debit :{balance,5}");
                    Console.WriteLine($"Amount to remove     :{amount,5}");
                    balance = balance - amount;
                  
                    Console.WriteLine($"Balance after debit  :{balance,5}");
                    return amount;

            }
        }

        public void Credit(decimal amount)
        {
            lock (balanceLock)
            {
                Console.WriteLine($"Balance before credit:{balance,5}");
                Console.WriteLine($"Amount to add        :{amount,5}");
                balance = balance + amount;
                Console.WriteLine($"Balance after credit :{balance,5}");
            }
        }
    }
}

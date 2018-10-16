using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ParallelExamples.ThreadExamples
{
    public class Bagno
    {
        private List<String> rotoli = new List<string>();
        public static string[] colors = {"Rosso", "Bianco", "Giallo" };
        private Random dado = new Random();
        public Dictionary<String, int> RotoliConsumati { get; } 
            = new Dictionary<string, int>();
        public bool AllNeededHaveBeenConsumed
        {
            get
            {
                return RotoliConsumati.Values.Sum() >= 6;
            }
                
        }

        public void Evacua(string color)
        {
            Console.WriteLine($" { Thread.CurrentThread.Name} sto cercando di ottenere il lock per entrare nel bagno");
            lock (this)
            {

                while (!rotoli.Remove(color))
                {
                    if (rotoli.Count > 0)
                    {
                        Console.WriteLine($" { Thread.CurrentThread.Name} Cacchio {color} non è il colore giusto!");
                    }
                    else
                    {
                        Console.WriteLine($" { Thread.CurrentThread.Name} Mai che ci siano rotoli in sto bagno, fannullone di un programmatore Jr!");
                    }
                    Console.WriteLine($" { Thread.CurrentThread.Name} Mi tocca abbioccarmi in bagno");
                    Monitor.Wait(this);
                    Console.WriteLine($" { Thread.CurrentThread.Name} Risvegliato! Vai che forse si evacua!");

                }
                if(RotoliConsumati.ContainsKey(color))
                {
                    RotoliConsumati[color]++;
                }
                else
                {
                    RotoliConsumati[color] = 1;
                }
                Console.WriteLine($" { Thread.CurrentThread.Name} Aaaah, ho evacuato con il rotolo {color}");

            }
        }

        public void NonHoStudiatoAbbastanzaProgrammazione()
        {
            Console.WriteLine($" { Thread.CurrentThread.Name} sto cercando di ottenere il lock per entrare nel bagno");
            lock (this)
            {
                string color = colors[dado.Next(colors.Length)];
                rotoli.Add(color);
                Monitor.PulseAll(this);
                Console.WriteLine($" { Thread.CurrentThread.Name} Ho fatto pulse e aggiunto un rotolo { color }");
            }
        }

    }
}

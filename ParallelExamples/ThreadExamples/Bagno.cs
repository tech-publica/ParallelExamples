using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ParallelExamples.ThreadExamples
{
    public class Bagno
    {
        private List<String> rotoli = new List<string>();
        public static string[] colors = {"Rosso", "Bianco", "Giallo" };
        private Random dado = new Random();

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
                    Monitor.Wait(this);                   
                                        
                }
                Console.WriteLine($" { Thread.CurrentThread.Name} Ho evacuato con il rotolo {color}");

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

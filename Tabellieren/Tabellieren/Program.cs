using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tabellieren
{
    class Program
    {
        static void Main(string[] args)
        {
            CSVTabellieren tabellieren = new CSVTabellieren();

            // Dateipfad finden
            string directory = Directory.GetCurrentDirectory();
            string pfad = directory + "\\Data.csv";
            Console.WriteLine("Pfad ist: " + pfad);
            Console.WriteLine();

            if (!File.Exists(pfad))
            {
                Console.WriteLine("File nicht existiert");
            }
            else
            {
                string[] zeilen = File.ReadAllLines(pfad);
                Console.WriteLine("CSV Datei hat insgesamt " + zeilen.Length + " Einträge");
                IEnumerable<string> tabellen = tabellieren.Tabellieren(zeilen);

                if (tabellen != null)
                {
                    foreach(var tabelle in tabellen)
                    {
                        Console.WriteLine(tabelle);
                    }
                }
            }
        }
    }
}

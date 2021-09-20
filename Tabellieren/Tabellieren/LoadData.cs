using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Tabellieren
{
    class LoadData : ILoad
    {
        public string[] Data()
        {
            // Search for data path
            string directory = Directory.GetCurrentDirectory();
            string pfad = directory + "\\Data.csv";
            Console.WriteLine("Path is: " + pfad);
            Console.WriteLine();

            if (!File.Exists(pfad))
            {
                Console.WriteLine("File not exist!");
                return null;
            }
            else
            {
                string[] zeilen = File.ReadAllLines(pfad);
                Console.WriteLine("CSV-Data have overall " + zeilen.Length + " entries");
                return zeilen;
            }
        }
    }
}

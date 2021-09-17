using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvTabellieren
{
    class CSVTabellieren
    {
        static void Main(string[] args)
        {
            string directory = Directory.GetCurrentDirectory();
            int[] stringLength;
            string[] headerString = new string[50];
            string[,] tabelle;
            string[] tmp;
            bool head = false;
            int stringCount = 0;
            StringBuilder builder;

            string pfad = directory + "\\Data.csv";
            Console.WriteLine("Pfad ist: " + pfad);
            Console.WriteLine();

            if (!File.Exists(pfad))
            {
                Console.WriteLine("File nicht existiert");
            }

            else
            {
                // Datei einlesen und in Variablen zuweisen
                string[] zeilen = File.ReadAllLines(pfad);
                Console.WriteLine("CSV Datei hat insgesamt " + zeilen.Length + " Einträge");
                string[] spalten = zeilen[0].Split(';');
                stringLength = new int[spalten.Length];

                // Daten in eine zweidimensional Array erstellen
                tabelle = new string[zeilen.Length, spalten.Length];
                for(int row = 0; row < zeilen.Length; row++)
                {
                    tmp = zeilen[row].Split(';');
                    for(int column = 0; column < spalten.Length; column++)
                    {
                        tabelle[row , column] = tmp[column];
                    }
                    //tmp = null;
                }

                // Anzahl größten Stringlänge per Spalte berechnen
                for (int column = 0; column < spalten.Length; column++)
                {
                    for (int row = 0; row < zeilen.Length; row++)
                    {
                        if (stringCount < tabelle[row, column].Length)
                        {
                            stringCount = tabelle[row, column].Length;
                        }
                    }
                    stringLength[column] = stringCount;
                    stringCount = 0;
                    Console.WriteLine("Größte Länge von " + column + " Spalte: " + stringLength[column]);
                }
                Console.WriteLine();

                // Tabelle ausgeben
                for(int i = 0; i < zeilen.Length; i++)
                {
                    for(int j = 0; j < spalten.Length; j++)
                    {
                        Console.WriteLine(tabelle[i , j]);
                    }
                    Console.WriteLine();
                }

                // Daten tabellieren
                for (int row = 0; row < zeilen.Length; row++)
                {
                    string[] toString = new string[50];
                    for (int column = 0; column < spalten.Length; column++)
                    {
                        builder = new StringBuilder(tabelle[row, column]);
                        if (tabelle[row, column].Length < stringLength[column])
                        {
                            builder.Insert(tabelle[row, column].Length, " ", stringLength[column] - (tabelle[row, column].Length));
                        }
                        builder.Append("|");
                        toString[column] = builder.ToString();

                        if (head == false)
                        {
                            builder = new StringBuilder();
                            builder.Insert(0, "_", stringLength[column]);
                            builder.Append("+");
                            headerString[column] = builder.ToString();
                        }
                    }
                    Console.WriteLine(ToString(toString));
                    if (head == false)
                    {
                        Console.WriteLine(ToString(headerString));
                        head = true;
                    }
                }
            }
        }

        public static string ToString(string[] daten)
        {
            StringBuilder tmp = new StringBuilder();
            for (int i = 0; i < daten.Length; i++)
            {
                tmp.Append(daten[i]);
            }
            return tmp.ToString();
        }


    }
}

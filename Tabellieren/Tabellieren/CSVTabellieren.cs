using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Tabellieren
{
    class CSVTabellieren
    {
        static void Main(string[] args)
        {
            StringBuilder builder;

            // Maximal Länge allen Spalten
            int[] stringLength;
            int stringCount = 0;

            // Hilfsvariablen für die Erstellung den Zeichen des Headers, auf 50 Spalten limitiert
            string[] headerString = new string[50];
            bool head = false;

            // 2-dimensionales Array für Speicherung von Daten
            string[,] tabelle;

            // temporäres Array als Hilfmittel für die Erstellung der Tabelle
            string[] tmp;

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
                // Datei einlesen und in Variablen zuweisen
                // [] zeilen : speichert allen Zeilen der CSV-Datei
                // [] spalten : Auswahl für die Berechnung die zu den bearbeiteten Spalten der Tabelle (Wieviele Spalten soll angezeigt werden) 
                string[] zeilen = File.ReadAllLines(pfad);
                Console.WriteLine("CSV Datei hat insgesamt " + zeilen.Length + " Einträge");
                string[] spalten = zeilen[0].Split(';');
                stringLength = new int[spalten.Length];

                // Daten in eine zweidimensional Array erstellen
                tabelle = new string[zeilen.Length, spalten.Length];
                for (int row = 0; row < zeilen.Length; row++)
                {
                    // workingArr erstellt mit null-Werte
                    string[] workingArr = new string[spalten.Length];
                    tmp = zeilen[row].Split(';');

                    // Daten vom tmp in workingArr kopieren
                    for (int column = 0; column < spalten.Length; column++)
                    {
                        if (workingArr.Length < tmp.Length)
                        {
                            Array.Copy(tmp, 0, workingArr, 0, workingArr.Length);
                        }
                        else
                        {
                            Array.Copy(tmp, 0, workingArr, 0, tmp.Length);
                        }

                        // Für Zeilen mit leeren Werten dann " " zuweisen
                        if (workingArr[column] == null)
                        {
                            workingArr[column] = " ";
                        }

                        tabelle[row, column] = workingArr[column];
                    }
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

                // Daten tabellieren
                for (int row = 0; row < zeilen.Length; row++)
                {
                    string[] toString = new string[50];
                    for (int column = 0; column < spalten.Length; column++)
                    {
                        builder = new StringBuilder(tabelle[row, column]);
                        if (tabelle[row, column].Length < stringLength[column])
                        {
                            // Leerzeichen am Ende jeden String hinzufügen, if der String < maximale Stringlänge der Spalte
                            builder.Insert(tabelle[row, column].Length, " ", stringLength[column] - (tabelle[row, column].Length));
                        }
                        builder.Append("|");
                        toString[column] = builder.ToString();

                        // Für die Darstellung den Zeichen unter Titelname
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

        // Daten aus einer Zeile ausgeben
        public static string ToString(string[] daten)
        {
            StringBuilder tmp = new StringBuilder();
            for (int i = 0; i < daten.Length; i++)
            {
                tmp.Append(daten[i]);
            }
            return tmp.ToString();
        }

        // Tabelle ausgeben
        //for(int i = 0; i < zeilen.Length; i++)
        //{
        //    for(int j = 0; j < spalten.Length; j++)
        //    {
        //        Console.WriteLine(tabelle[i , j]);
        //    }
        //    Console.WriteLine();
        //}
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tabellieren
{
    class CSVTabellieren : ICSV
    {
        // Maximal Länge allen Spalten
        private int[] stringLength;

        // 2-dimensionales Array für Speicherung von Daten
        private string[,] tabelle;

        // Zeilen und Spalten der Tabelle
        private string[] zeilen;
        private string[] spalten;

        public IEnumerable<string> Tabellieren(Data data)
        {
            try
            {
                try
                {
                    Console.WriteLine(data.ToString());
                    Console.WriteLine();
                    string[] CSV_zeilen = File.ReadAllLines(data.Path);
                    //Console.WriteLine("CSV-Data have overall " + CSV_zeilen.Length + " entries");
                    TabellenErstellen(CSV_zeilen, data.Symbol);
                    StringLengthEachColumn();
                    return DatenBearbeiten();
                }
                catch (Exception ex)
                {
                    throw new CustomException(ex.Message, ex);
                }
            }
            catch (CustomException ex)
            {
                Console.WriteLine(CustomException.CustomMessage(ex));
                Console.WriteLine("Table cannot be created!");
                return null;
            }
        }

        // Daten tabellieren
        private IEnumerable<string> DatenBearbeiten()
        {
            StringBuilder builder;

            // Ergebnis jede Spalte wird hier gespeichert
            List<string> results = new List<string>();

            // Hilfsvariablen für die Erstellung den Zeichen des Headers
            string[] headerString = new string[spalten.Length];
            bool head = false;

            for (int row = 0; row < zeilen.Length; row++)
            {
                //String array for the output, create one per row
                string[] toString = new string[spalten.Length];

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
                        headerString[column] = builder.ToString(); // ToString() here ist der Methode von StringBuilder-Klasse
                    }
                }
                results.Add(ToString(toString)); // eigene ToString()-Methode
                if (head == false)
                {
                    results.Add(ToString(headerString));
                    head = true;
                }
            }
            return results;
        }

        // Tabellen erstellen
        private void TabellenErstellen(IEnumerable<string> CSV_zeilen, string symbol)
        {
            // Datei in Variablen zuweisen
            // [] zeilen : speichert allen Zeilen der CSV-Datei
            // [] spalten : Auswahl für die Berechnung die zu den bearbeiteten Spalten der Tabelle (Wieviele Spalten soll angezeigt werden)
            zeilen = CSV_zeilen.ToArray();
            spalten = zeilen[0].Split(symbol);
            stringLength = new int[spalten.Length];
            tabelle = new string[zeilen.Length, spalten.Length];

            // temporäres Array als Hilfmittel für die Erstellung der Tabelle
            string[] tmp;

            // Daten in eine zweidimensional Array erstellen
            for (int row = 0; row < zeilen.Length; row++)
            {
                // workingArr erstellt mit null-Werte
                string[] workingArr = new string[spalten.Length];
                tmp = zeilen[row].Split(symbol);

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
        }

        // Anzahl größten Stringlänge per Spalte berechnen
        private void StringLengthEachColumn()
        {
            int stringCount = 0;
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
                //Console.WriteLine("Größte Länge von " + column + " Spalte: " + stringLength[column]);
            }
            Console.WriteLine();
        }

        // Strings aus einer Zeile zusammenfügen
        private string ToString(string[] daten)
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

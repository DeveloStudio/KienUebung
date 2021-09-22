using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tabellieren
{
    class CSVTabellieren : ICSV
    {
        private StringBuilder builder;

        // Maximal Länge allen Spalten
        private int[] stringLength;
        private int stringCount = 0;

        // Hilfsvariablen für die Erstellung den Zeichen des Headers, auf 50 Spalten limitiert
        private string[] headerString = new string[50];
        private bool head = false;

        // 2-dimensionales Array für Speicherung von Daten
        private string[,] tabelle;

        // Zeilen und Spalten der Tabelle
        private string[] zeilen;
        private string[] spalten;

        public IEnumerable<string> Tabellieren(IEnumerable<string> CSV_zeilen)
        {
            return DatenBearbeiten(CSV_zeilen);
        }

        // Daten tabellieren
        private IEnumerable<string> DatenBearbeiten(IEnumerable<string> CSV_zeilen)
        {
            TabellenErstellen(CSV_zeilen);

            // Ergebnis jede Spalte wird hier gespeichert
            List<string> results = new List<string>();

            for (int row = 0; row < zeilen.Length; row++)
            {
                //String Array für die Ausgabe, auf 50 Spalten limitiert
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
        private void TabellenErstellen(IEnumerable<string> CSV_zeilen)
        {
            // Datei in Variablen zuweisen
            // [] zeilen : speichert allen Zeilen der CSV-Datei
            // [] spalten : Auswahl für die Berechnung die zu den bearbeiteten Spalten der Tabelle (Wieviele Spalten soll angezeigt werden)
            zeilen = CSV_zeilen.ToArray();
            spalten = zeilen[0].Split(";");
            stringLength = new int[spalten.Length];
            tabelle = new string[zeilen.Length, spalten.Length];

            // temporäres Array als Hilfmittel für die Erstellung der Tabelle
            string[] tmp;

            // Daten in eine zweidimensional Array erstellen
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
            StringLengthEachColumn();
        }

        // Anzahl größten Stringlänge per Spalte berechnen
        private void StringLengthEachColumn()
        {
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

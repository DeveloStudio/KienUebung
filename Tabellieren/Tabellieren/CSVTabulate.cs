using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tabulate
{
    public class CSVTabulate : ICSV
    {
        // 2-dimensional array for storage data
        private string[,] table;

        public IEnumerable<string> Tabulate(Data data)
        {
            try
            {
                try
                {
                    Console.WriteLine(data.ToString());
                    Console.WriteLine();
                    string[] CSV_rows = File.ReadAllLines(data.Path);
                    table = CreateTable(CSV_rows, data.Symbol);
                    StringLengthEachColumn(table);
                    return DataEdit(table);
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
                Console.WriteLine("--------------------");
                return null;
            }
        }

        // Table create
        public string[,] CreateTable(IEnumerable<string> CSV_rows, string symbol)
        {
            string[,] tabletmp;

            // Data in variables assign
            // [] rows : stored all rows 
            // [] columns : stored the first row, splited by symbol
            string[] rows = (string[])CSV_rows;
            string[] columns = rows[0].Split(symbol);
            tabletmp = new string[rows.Length, columns.Length];

            // temporary array for creating the table
            string[] tmp;

            // Create data in a two-dimensional array
            for (int row = 0; row < rows.Length; row++)
            {
                // workingArr created with null values
                string[] workingArr = new string[columns.Length];

                tmp = rows[row].Split(symbol);

                // Copy data from tmp to workingArr+
                if (workingArr.Length < tmp.Length)
                {
                    Array.Copy(tmp, 0, workingArr, 0, workingArr.Length);
                }
                else
                {
                    Array.Copy(tmp, 0, workingArr, 0, tmp.Length);
                }

                // Copy data from workingArr to tabletmp
                for (int column = 0; column < columns.Length; column++)
                {
                    // Assign " " for rows with empty values
                    if (workingArr[column] == null)
                    {
                        workingArr[column] = "isNull";
                    }

                    if (workingArr[column] == string.Empty)
                    {
                        workingArr[column] = "isEmpty";
                    }

                    tabletmp[row, column] = workingArr[column];
                }
            }
            return tabletmp;
        }

        // Calculate the number of largest string lengths per column
        public int[] StringLengthEachColumn(string[,] table)
        {
            int rows = table.GetLength(0);
            int columns = table.GetLength(1);

            // Maximum length all columns, each array entry stores a matching maximum length
            int[] stringLength = new int[columns];

            int stringCount = 0;
            for (int column = 0; column < columns; column++)
            {
                for (int row = 0; row < rows; row++)
                {
                    if (stringCount < table[row, column].Length)
                    {
                        stringCount = table[row, column].Length;
                    }
                }
                stringLength[column] = stringCount;
                stringCount = 0;
                //Console.WriteLine("Größte Länge von " + column + " Spalte: " + stringLength[column]);
            }
            return stringLength;
        }

        // Data tabulate
        public IEnumerable<string> DataEdit(string[,] table)
        {
            StringBuilder builder;

            // Result each columns is stored here
            List<string> results = new List<string>();

            // Variables for creating the characters of the header
            string[] headerString = new string[columns.Length];
            bool head = false;

            for (int row = 0; row < rows.Length; row++)
            {
                //String array for the output, create one per row
                string[] toString = new string[columns.Length];

                for (int column = 0; column < columns.Length; column++)
                {
                    builder = new StringBuilder(table[row, column]);
                    if (table[row, column].Length < stringLength[column])
                    {
                        // Add spaces at the end of each string, if the string < maximal stringlength of the column 
                        builder.Insert(table[row, column].Length, " ", stringLength[column] - (table[row, column].Length));
                    }
                    builder.Append("|");
                    toString[column] = builder.ToString();

                    // For the representation of the characters under title name
                    if (head == false)
                    {
                        builder = new StringBuilder();
                        builder.Insert(0, "_", stringLength[column]);
                        builder.Append("+");
                        headerString[column] = builder.ToString(); // ToString() here ist the method from StringBuilder class
                    }
                }
                results.Add(ToString(toString)); // own ToString() method
                if (head == false)
                {
                    results.Add(ToString(headerString));
                    head = true;
                }
            }
            return results;
        }

        // Merge strings from one row
        public string ToString(string[] daten)
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

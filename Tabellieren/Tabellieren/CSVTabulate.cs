using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tabulate
{
    class CSVTabulate : ICSV
    {
        // Maximum length all columns, each array entry stores a matching maximum length
        private int[] stringLength;

        // 2-dimensional array for storage data
        private string[,] table;

        // Table rows and columns
        private string[] rows;
        private string[] columns;

        public IEnumerable<string> Tabulate(Data data)
        {
            try
            {
                try
                {
                    Console.WriteLine(data.ToString());
                    Console.WriteLine();
                    string[] CSV_rows = File.ReadAllLines(data.Path);
                    //Console.WriteLine("CSV-Data have overall " + CSV_zeilen.Length + " entries");
                    CreateTable(CSV_rows, data.Symbol);
                    StringLengthEachColumn();
                    return DataEdit();
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

        // Data tabulate
        private IEnumerable<string> DataEdit()
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

        // Table create
        private void CreateTable(IEnumerable<string> CSV_rows, string symbol)
        {
            // Data in variables assign
            // [] rows : stored all rows 
            // [] columns : stored the first row, splited by symbol
            rows = CSV_rows.ToArray();
            columns = rows[0].Split(symbol);
            stringLength = new int[columns.Length];
            table = new string[rows.Length, columns.Length];

            // temporary array for creating the table
            string[] tmp;

            // Create data in a two-dimensional array
            for (int row = 0; row < rows.Length; row++)
            {
                // workingArr created with null values
                string[] workingArr = new string[columns.Length];
                tmp = rows[row].Split(symbol);

                // Copy data from tmp to workingArr
                for (int column = 0; column < columns.Length; column++)
                {
                    if (workingArr.Length < tmp.Length)
                    {
                        Array.Copy(tmp, 0, workingArr, 0, workingArr.Length);
                    }
                    else
                    {
                        Array.Copy(tmp, 0, workingArr, 0, tmp.Length);
                    }

                    // Assign " " for rows with empty values
                    if (workingArr[column] == null)
                    {
                        workingArr[column] = " ";
                    }

                    table[row, column] = workingArr[column];
                }
            }
        }

        // Calculate the number of largest string lengths per column
        private void StringLengthEachColumn()
        {
            int stringCount = 0;
            for (int column = 0; column < columns.Length; column++)
            {
                for (int row = 0; row < rows.Length; row++)
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
            Console.WriteLine();
        }

        // Merge strings from one row
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

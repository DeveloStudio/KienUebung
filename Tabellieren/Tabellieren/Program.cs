using System;
using System.Collections.Generic;

namespace Tabulate
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                try
                {
                    // Interface for create a table
                    ICSV tabulate = new CSVTabulate();

                    // Interface for load data
                    ILoad loadData = new LoadData();

                    // Interface for print data to the console
                    IOutput Output = new MyOutput();

                    // Load data from local computer
                    Data data = loadData.Load();

                    // Create table
                    IEnumerable<string> table = tabulate.Tabulate(data);

                    // Print data
                    Output.Out(table);
                }
                catch (Exception ex)
                {
                    throw new CustomException(ex.Message, ex);
                }
            }
            catch (CustomException ex)
            {
                Console.WriteLine(CustomException.CustomMessage(ex));
            }

        }
    }
}

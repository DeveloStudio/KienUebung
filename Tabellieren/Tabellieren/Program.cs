using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Tabulate
{
    class Program
    {
        static void Main(string[] args)
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
    }
}

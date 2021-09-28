using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Tabellieren
{
    class Program
    {
        static void Main(string[] args)
        {
            ICSV tabellieren = new CSVTabellieren();

            ILoad loadData = new LoadData();

            IOutput Output = new MyOutput();

            Data data = loadData.Load();

            IEnumerable<string> tabellen = tabellieren.Tabellieren(data);

            Output.Out(tabellen);
        }
    }
}

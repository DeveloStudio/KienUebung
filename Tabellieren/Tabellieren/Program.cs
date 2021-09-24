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

            string[] datas = loadData.Data();

            IEnumerable<string> tabellen = tabellieren.Tabellieren(datas);

            Output.Out(tabellen);
        }
    }
}

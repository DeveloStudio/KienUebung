using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tabellieren
{
    class Program
    {
        static void Main(string[] args)
        {
            ICSV tabellieren = new CSVTabellieren();

            ILoad loadData = new LoadData();

            string[] datas = loadData.Data();

            if (datas != null)
            {
                IEnumerable<string> tabellen = tabellieren.Tabellieren(datas);
            }

            if (datas != null)
            {
                foreach (var data in datas)
                {
                    Console.WriteLine(data);
                }
            }
        }
    }
}

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
            //ICSV tabellieren = new CSVTabellieren();

            //ILoad loadData = new LoadData();

            //IOutput Output = new MyOutput();

            //string[] datas = loadData.Data();

            //IEnumerable<string> tabellen = tabellieren.Tabellieren(datas);

            //Output.Out(tabellen);
            try
            {
                try
                {
                    StreamReader reader = new StreamReader(@"C: \Users\nguyen\Documents\GitHub\KienUebung\Tabellieren\Tabellieren\bin\Debug\netcoreapp3.1\Data.txt");
                    Console.WriteLine(reader.ReadToEnd());
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

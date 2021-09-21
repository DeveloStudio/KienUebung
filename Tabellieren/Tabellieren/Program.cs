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
            //ICSV tabellieren = new CSVTabellieren();

            //ILoad loadData = new LoadData();

            //IOutputInf Output = new MyOutput();

            //string[] datas = loadData.Data();

            //IEnumerable<string> tabellen = tabellieren.Tabellieren(datas);

            //Output.Out(tabellen);
            try
            {
                try
                {
                    StreamReader reader = new StreamReader(@"C: \Users\nguyen\Documents\GitHub\KienUebung\Tabellieren\Tabellieren\bin\Debug\netcoreapp3.1\Data1.txt");
                    Console.WriteLine(reader.ReadToEnd());
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.ToString());
                    //Console.WriteLine(ex.Message);
                    //Console.WriteLine(ex.MyMessage());
                    throw new ICustomException("I catched this: " + ex.Message, ex);
                    //ex.Message;
                }
            }
            catch(ICustomException ex)
            {
                //Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.InnerException.GetType().Name);
            }
           
        }
    }
}

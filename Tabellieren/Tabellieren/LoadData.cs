using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Tabulate
{
    public class LoadData : ILoad
    {
        public Data Load()
        {
            string symbol = " ";
            int i = -1;

            // Ask for data path and symbol
            string path = Directory.GetCurrentDirectory();
            Console.WriteLine("The file you want to open: ");
            string file = Console.ReadLine();
            Console.WriteLine();

            while (i < 0)
            {
                Console.WriteLine("The symbol you want your file to be separate (Only accept ; or , or .): ");
                symbol = Console.ReadLine();
                if (symbol.Equals(";") || symbol.Equals(",") || symbol.Equals("."))
                {
                    i++;
                }
            }
            Console.WriteLine();

            return CreateDataPath(path, file, symbol);
        }

        public static Data CreateDataPath(string path, string filename, string symbol)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(path);
            builder.Append("\\");
            builder.Append(filename);

            return new Data(builder.ToString(), symbol);
        }

        //try
        //{
        //    try
        //    {
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new CustomException(ex.Message, ex);
        //    }
        //}
        //catch (CustomException ex)
        //{
        //    Console.WriteLine(CustomException.CustomMessage(ex));
        //    return null;
        //}
    }
}

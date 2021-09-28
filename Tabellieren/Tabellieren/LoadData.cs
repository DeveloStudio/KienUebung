using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Tabulate
{
    class LoadData : ILoad
    {
        public Data Load()
        {
            Data data = FindData();

            try
            {
                try
                {
                    return data;
                }
                catch (Exception ex)
                {
                    throw new CustomException(ex.Message, ex);
                }
            }
            catch (CustomException ex)
            {
                Console.WriteLine(CustomException.CustomMessage(ex));
                return null;
            }
        }

        private Data FindData()
        {
            StringBuilder builder = new StringBuilder();
            string symbol = " ";
            int i = -1;

            // Ask for data path and symbol
            string directory = Directory.GetCurrentDirectory();
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

            builder.Append(directory);
            builder.Append("\\");
            builder.Append(file);

            return new Data(builder.ToString(), symbol);
        }
    }
}

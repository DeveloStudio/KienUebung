using System;
using System.Collections.Generic;
using System.Text;

namespace Tabellieren
{
    class MyOutput : IOutput
    {
        public void Out(IEnumerable<string> datas)
        {
            if(datas != null)
            {
                foreach (string data in datas)
                {
                    try
                    {
                        try
                        {
                            Console.WriteLine(data);
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
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Cannot print the data, something is wrong! Please check and try again!");
                Console.WriteLine();
            }
        }
    }
}

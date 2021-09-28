using System;
using System.Collections.Generic;
using System.Text;

namespace Tabulate
{
    class MyOutput : IOutput
    {
        public MyOutput()
        {

        }

        public void Out(IEnumerable<string> datas)
        {
            if (datas is null)
            {
                throw new ArgumentNullException(nameof(datas));
            }

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
    }
}

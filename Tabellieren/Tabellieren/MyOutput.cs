using System;
using System.Collections.Generic;
using System.Text;

namespace Tabellieren
{
    class MyOutput : IOutput
    {
        public void Out(IEnumerable<string> datas)
        {
            try
            {
                try
                {
                    foreach (string data in datas)
                    {
                        Console.WriteLine(data);
                    }
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

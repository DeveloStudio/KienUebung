using System;
using System.Collections.Generic;
using System.Text;

namespace Tabulate
{
    public class MyOutput : IOutput
    {
        public void Out(IEnumerable<string> datas)
        {
            if (datas == null)
            {
                throw new CustomException("Data is null");
            }

            foreach (string data in datas)
            {
                Console.WriteLine(data);
            }
        }
    }
}

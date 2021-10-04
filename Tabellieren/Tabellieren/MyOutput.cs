using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Tabulate
{
    public class MyOutput : IOutput
    {
        public void Out(IEnumerable<string> datas)
        {
            if (datas == null)
            {
                throw new NullReferenceException("From class MyOutPut under the method " + MethodBase.GetCurrentMethod() + ": Data is null");
            }

            foreach (string data in datas)
            {
                Console.WriteLine(data);
            }
        }
    }
}

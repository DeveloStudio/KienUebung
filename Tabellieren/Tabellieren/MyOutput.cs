﻿using System;
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
                foreach(var data in datas)
                {
                    Console.WriteLine(data);
                }
            }
        }
    }
}

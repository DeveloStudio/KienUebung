using System;
using System.Collections.Generic;
using System.Text;

namespace Tabellieren
{
    interface IOutput
    {
        public void Out (IEnumerable<string> datas);
    }
}

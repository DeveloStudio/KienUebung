using System;
using System.Collections.Generic;
using System.Text;

namespace Tabulate
{
    interface IOutput
    {
        public void Out(IEnumerable<string> datas);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Tabulate
{
    interface ICSV
    {
        IEnumerable<string> Tabulate(Data data);
    }
}

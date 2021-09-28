using System;
using System.Collections.Generic;
using System.Text;

namespace Tabellieren
{
    interface ICSV
    {
        IEnumerable<string> Tabellieren(Data data);
    }
}

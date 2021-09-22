using System;
using System.Collections.Generic;
using System.Text;

namespace Tabellieren
{
    interface ICSV
    {
        IEnumerable<string> Tabellieren(IEnumerable<string> CSV_zeilen);
    }
}

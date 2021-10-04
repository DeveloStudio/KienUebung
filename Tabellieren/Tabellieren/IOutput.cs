using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Tabulate
{
    interface IOutput
    {
        public void Out(IEnumerable<string> datas);
    }
}

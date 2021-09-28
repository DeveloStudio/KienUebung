using System;
using System.Collections.Generic;
using System.Text;

namespace Tabellieren
{
    class Data
    {
        private string path;
        private string symbol;

        public Data(string path, string symbol)
        {
            this.path = path;
            this.symbol = symbol;
        }

        // Property for private string path
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
            }
        }

        // Property for private string symbol
        public string Symbol
        {
            get { return symbol; }
            set
            {
                symbol = value;
            }
        }

        public override string ToString()
        {
            return "Path of the data is: " + this.path + " and the Symbol is: " + this.symbol;
        }
    }
}

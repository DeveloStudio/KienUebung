using System;
using System.Collections.Generic;
using System.Text;

namespace Tabulate
{
    public class Data
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

        public override bool Equals(object obj)
        {
            // if the passed object is null
            if (obj == null)
            {
                return false;
            }
            // if the passed object is not type Data
            if (!(obj is Data))
            {
                return false;
            }
            // Check equals
            return (this.path == ((Data)obj).Path) && (this.symbol == ((Data)obj).Symbol);
        }

        //Override the GetHashCode method to allow a type to work correctly in a hash table
        public override int GetHashCode()
        {
            return HashCode.Combine(path, symbol, Path, Symbol);
        }

        public override string ToString()
        {
            return "Path of the data is: " + this.path + " and the Symbol is: " + this.symbol;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Tabellieren
{
    class ICustomException : Exception
    {
        public ICustomException () : base () { }

        public ICustomException (string message) : base (message) { }

        public ICustomException(string message, Exception innerException) : base (message, innerException) { }

        public string MyMessage()
        {
            string message = "Hallo " + base.Message;
            return message;
        }

    }
}

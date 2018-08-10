using System;
using System.Collections.Generic;
using System.Text;

namespace De.Markellus.Matrix.Exceptions
{
    /// <summary>
    /// It was this moment when the developer knew: he f*cked up
    /// </summary>
    public class WrongImplementationException : Exception
    {
        public WrongImplementationException(string message)
            : base("Wrong Implementation: " + message)
        {
        }
    }
}

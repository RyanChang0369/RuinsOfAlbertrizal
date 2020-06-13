using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Exceptions
{
    public class NotEnoughManaException : Exception
    {
        public NotEnoughManaException() { }

        public NotEnoughManaException(string message) : base(message) { }

        public NotEnoughManaException(string message, Exception inner) : base(message, inner) { }
    }
}

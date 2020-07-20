using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Exceptions
{
    public class CannotTargetException : Exception
    {
        public CannotTargetException() { }

        public CannotTargetException(string message) : base(message) { }

        public CannotTargetException(string message, Exception inner) : base(message, inner) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Exceptions
{
    /// <summary>
    /// Thrown if AI fails to move the enemy to a different location.
    /// </summary>
    public class DidNotMoveException : Exception
    {
        public DidNotMoveException() { }

        public DidNotMoveException(string message) : base(message) { }

        public DidNotMoveException(string message, Exception inner) : base(message, inner) { }
    }
}

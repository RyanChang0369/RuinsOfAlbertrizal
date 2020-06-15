using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Any object that has to respond to StartRound and/or EndRound
    /// </summary>
    public interface IRoundBasedObject : ITurnBasedObject
    {
        void StartRound();

        void EndRound();
    }
}

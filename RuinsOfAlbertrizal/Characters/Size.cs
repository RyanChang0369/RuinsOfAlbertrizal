using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Characters
{
    class Size
    {
        public enum Sizes
        {
            xxSmall = -3,
            xSmall = -2,
            Small = -1,
            HumanSized = 0,
            Regular = 0,
            Large = 1,
            xLarge = 2,
            xxLarge = 3
        }

        public bool CanFit()
        {
            return false;
        }
    }
}

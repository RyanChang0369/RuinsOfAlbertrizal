using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Characters
{
    public class Player : Character
    {
        private Player()
        {

        }
        public Player(string generalName, string specificName, string description, int[] baseValues)
            : base(generalName, specificName, description, baseValues)
        {

        }
    }
}

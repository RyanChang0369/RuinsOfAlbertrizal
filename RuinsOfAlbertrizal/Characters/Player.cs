using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RuinsOfAlbertrizal.Characters
{
    public class Player : Character
    {
        public Player()
        {

        }

        ///// <summary>
        ///// Create a new level 1 player
        ///// </summary>
        ///// <param name="generalName"></param>
        ///// <param name="specificName"></param>
        ///// <param name="description"></param>
        ///// <param name="baseValues"></param>
        //public Player(string generalName, string specificName, string description, int[] baseValues)
        //    : base(generalName, specificName, description, 1, baseValues)
        //{

        //}

        public override void Die()
        {
            MessageBox.Show("You have died...");
            throw new NotImplementedException();
        }
    }
}

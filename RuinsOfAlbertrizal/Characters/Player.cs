using RuinsOfAlbertrizal.AIs;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Items;
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
            AIStyle = AI.AIStyle.Player;
        }

        public override void Die()
        {
            MessageBox.Show("You have died...");
            throw new NotImplementedException();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RuinsOfAlbertrizal.Characters
{
    public class Boss : Enemy
    {
        public Boss()
        {

        }

        public void SummonBoss()
        {
            
        }

        public override void Die()
        {
            GameBase.CurrentGame.AliveBosses.Remove(this);
            throw new NotImplementedException();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RuinsOfAlbertrizal.Characters
{
    [Serializable]
    public class Boss : Enemy
    {
        public Boss() : base()
        {

        }

        public void SummonBoss()
        {
            
        }

        public override void Die()
        {
            throw new NotImplementedException();
        }
    }
}

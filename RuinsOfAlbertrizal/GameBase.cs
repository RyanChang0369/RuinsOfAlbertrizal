using RuinsOfAlbertrizal.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    public static class GameBase
    {
        public static Map CurrentGame = new Map();
        
        public enum Stats
        {
            HP, Mana, Def, Dmg, Spd, Jump
        }
    }
}

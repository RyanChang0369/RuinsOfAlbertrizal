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
        public static string CustomMapLocation { get; set; }
        public static Map CurrentGame;
        
        /// <summary>
        /// Damage reduction through def = dmg/(def/10)
        /// </summary>
        public enum Stats
        {
            HP, Mana, Def, Dmg, Spd, Jump
        }

        public enum ClassType
        {
            NA, Warrior, Mage, Scout
        }

        public static int[] GetBaseValues(ClassType classType)
        {
            return GetBaseValues((int)classType - 1);
        }

        public static int[] GetBaseValues(int selectedIndex)
        {
            int[] numericalValues = new int[5];

            switch (selectedIndex)
            {
                case 0: //Warrior
                    numericalValues[0] = 200;
                    numericalValues[1] = 20;
                    numericalValues[2] = 10;
                    numericalValues[3] = 5;
                    numericalValues[4] = 5;
                    break;
                case 1: //Mage
                    numericalValues[0] = 150;
                    numericalValues[1] = 100;
                    numericalValues[2] = 2;
                    numericalValues[3] = 5;
                    numericalValues[4] = 7;
                    break;
                case 2: //Scout
                    numericalValues[0] = 100;
                    numericalValues[1] = 55;
                    numericalValues[2] = 2;
                    numericalValues[3] = 7;
                    numericalValues[4] = 10;
                    break;
            }

            return numericalValues;
        }

        public static ClassType GetClassType(int[] baseValues)
        {
            switch (baseValues[0])
            {
                case 200:
                    return ClassType.Warrior;
                case 150:
                    return ClassType.Mage;
                case 100:
                    return ClassType.Scout;
                default:
                    return ClassType.NA;
            }
        }

        public static void NewGame(Map map)
        {
            CurrentGame = map;
        }
    }
}

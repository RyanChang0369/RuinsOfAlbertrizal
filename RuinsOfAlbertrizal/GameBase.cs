using RuinsOfAlbertrizal.Environment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    public static class GameBase
    {
        public static string CurrentMapLocation { get; set; }

        public static string StaticMapLocation { get; set; }

        /// <summary>
        /// The game that is being played.
        /// </summary>
        public static Map CurrentGame;

        /// <summary>
        /// The game that is opened for editing.
        /// </summary>
        public static Map StaticGame;
        
        /// <summary>
        /// Damage reduction through def = dmg/(def/10)
        /// </summary>
        public enum Stats
        {
            HP, Mana, Def, Dmg, Spd
        }

        public enum ClassType
        {
            None = -1, Warrior, Mage, Scout
        }

        public static int[] GetBaseValues(ClassType classType)
        {
            return GetBaseValues((int)classType);
        }

        public static int[] GetBaseValues(int selectedIndex)
        {
            int[] numericalValues = new int[5];

            switch (selectedIndex)
            {
                case 0: //Warrior
                    numericalValues[0] = 200;
                    numericalValues[1] = 20;
                    numericalValues[2] = 20;
                    numericalValues[3] = 5;
                    numericalValues[4] = 10;
                    break;
                case 1: //Mage
                    numericalValues[0] = 150;
                    numericalValues[1] = 100;
                    numericalValues[2] = 2;
                    numericalValues[3] = 5;
                    numericalValues[4] = 14;
                    break;
                case 2: //Scout
                    numericalValues[0] = 100;
                    numericalValues[1] = 55;
                    numericalValues[2] = 2;
                    numericalValues[3] = 7;
                    numericalValues[4] = 35;
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
                    return ClassType.None;
            }
        }

        public static void NewGame()
        {
            CurrentGame = new Map();
            StaticGame = new Map();
        }

        public static void ResetCurrentGame()
        {
            CurrentGame = null;

        }
    }
}

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
            HP, Mana, Def, Dmg, Spd, Int, r1, r2, r3, r4, r5
        }

        public static string[] StatNames = { "HP", "Mana", "Defense", "Damage", "Speed", "Intellegence",
                    "Reserved1", "Reserved2", "Reserved3", "Reserved4", "Reserved5"};

        public enum ClassType
        {
            None = -1, Warrior, Mage, Scout
        }

        public const int TickSpeed = 20;


        public static int[] GetBaseValues(ClassType classType)
        {
            return GetBaseValues((int)classType);
        }

        public static int[] GetBaseValues(int selectedIndex)
        {
            int[] numericalValues = new int[10];

            switch (selectedIndex)
            {
                case 0: //Warrior
                    numericalValues[0] = 200;
                    numericalValues[1] = 20;
                    numericalValues[2] = 20;
                    numericalValues[3] = 13;
                    numericalValues[4] = 10;
                    numericalValues[5] = 20;
                    break;
                case 1: //Mage
                    numericalValues[0] = 150;
                    numericalValues[1] = 100;
                    numericalValues[2] = 2;
                    numericalValues[3] = 5;
                    numericalValues[4] = 14;
                    numericalValues[5] = 100;
                    break;
                case 2: //Scout
                    numericalValues[0] = 100;
                    numericalValues[1] = 55;
                    numericalValues[2] = 2;
                    numericalValues[3] = 17;
                    numericalValues[4] = 35;
                    numericalValues[5] = 55;
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

        /// <summary>
        /// Returns true if both the current and static games are properly initialized.
        /// </summary>
        /// <returns></returns>
        public static bool Initialized()
        {
            return CurrentGame != null && StaticGame != null;
        }
    }
}

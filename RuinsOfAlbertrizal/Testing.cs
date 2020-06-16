using RuinsOfAlbertrizal.Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Used for testing
    /// </summary>
    public class Testing
    {
        public static bool IsReleaseVersion;
        public static bool Debugging = true;

        private static void Test()
        {
            //Player player = new Player();
        }

        public static void InitTest()
        {
            IsReleaseVersion = !Debugging;
            if (Debugging)
            {
                Console.WriteLine("Testing...");
                Console.WriteLine("Console OK");
                Console.Beep(500, 100);
                Test();
            }

            if (!IsReleaseVersion)
            {
                Console.WriteLine("This program is not in a release version.");
                Console.Beep(700, 100);
            }
        }

        public static void ExploreInterfaceTest()
        {
            if (!Debugging)
                return;

            GameBase.CurrentGame.PlayerConsumables.Add(GameBase.CurrentGame.StoredConsumables[0]);
        }
    }
}

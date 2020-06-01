using System;
using System.Collections.Generic;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Used for testing
    /// </summary>
    public class Testing
    {
        public static bool IsReleaseVersion;
        public static bool Debugging = false;

        private static void Test()
        {
            
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

        public static void AdventureInterfaceTest()
        {
            GameBase.CurrentGame.PlayerEquiptments.Add(GameBase.CurrentGame.StoredEquiptments[0]);
        }
    }
}

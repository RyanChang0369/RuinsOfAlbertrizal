using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

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
            //bool[, ] table = new bool[BattleField.BattleFieldHeight, BattleField.BattleFieldWidth];

            //Point location = new Point(10, 1);
            //int moveRange = 2;

            //int rightEdge = Math.Max(location.X - moveRange + 1, 0);
            //int leftEdge = Math.Min(location.X + moveRange, BattleField.BattleFieldWidth);
            //int topEdge = Math.Max(location.Y - moveRange, 0);
            //int bottomEdge = Math.Min(location.Y + 2 * moveRange + 1, BattleField.BattleFieldHeight);

            //for (int i = topEdge; i < bottomEdge; i++)
            //{
            //    int jaggedRightEdge = Math.Max(rightEdge + Math.Abs(i - location.Y), 0);
            //    int jaggedLeftEdge = Math.Min(leftEdge - Math.Abs(i - location.Y) + 1, BattleField.BattleFieldWidth);
            //    for (int j = jaggedRightEdge; j <= jaggedLeftEdge; j++)
            //    {
            //        try
            //        {
            //            table[i, j] = true;
            //        }
            //        catch (IndexOutOfRangeException)
            //        {

            //        }
            //    }
            //}

            //for (int i = 0; i < BattleField.BattleFieldHeight; i++)
            //{
            //    for (int j = 0; j < BattleField.BattleFieldWidth; j++)
            //    {
            //        if (table[i, j])
            //        {
            //            Console.Write("* ");
            //        }
            //        else
            //        {
            //            Console.Write("x ");
            //        }
            //    }
            //    Console.WriteLine();
            //}
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
        }
    }
}

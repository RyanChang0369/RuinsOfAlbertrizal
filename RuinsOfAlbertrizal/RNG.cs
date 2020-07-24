using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    public static class RNG
    {
        private static Random RNGNum = new Random();

        /// <summary>
        /// Returns true based on percent chance given.
        /// </summary>
        /// <param name="percentChance">A number 0 - 100</param>
        /// <returns>True based on percent chance given.</returns>
        public static bool PercentChance(int percentChance)
        {
            return (RNGNum.Next(100) < percentChance);
        }

        /// <summary>
        /// Returns an integer ranging from 0 - 99.
        /// </summary>
        /// <returns>An integer ranging from 0 - 99.</returns>
        public static int GetRandomPercent()
        {
            return GetRandomInteger(100);
        }

        /// <summary>
        /// Returns an integer ranging from minValue, inclusive, to maxValue, exclusive.
        /// </summary>
        /// <returns>An integer ranging from minValue to maxValue - 1.</returns>
        public static int GetRandomInteger(int minVal, int maxVal)
        {
            return RNGNum.Next(maxVal - minVal) + minVal;
        }

        /// <summary>
        /// Returns an integer ranging from 0, inclusive, to maxValue, exclusive.
        /// </summary>
        /// <returns>An integer ranging from 0 to maxValue - 1.</returns>
        public static int GetRandomInteger(int maxVal)
        {
            return GetRandomInteger(0, maxVal);
        }

        /// <summary>
        /// Returns a double ranging from minValue, inclusive, to maxValue, exclusive.
        /// </summary>
        /// <returns>An integer ranging from minValue to maxValue - 1.</returns>
        public static double GetRandomDouble(double minVal, double maxVal)
        {
            return RNGNum.NextDouble() * (maxVal - minVal) + minVal;
        }

        /// <summary>
        /// Returns a double ranging from 0, inclusive, to maxValue, exclusive.
        /// </summary>
        /// <returns>An integer ranging from 0 to maxValue - 1.</returns>
        public static double GetRandomDouble(double maxVal)
        {
            return GetRandomDouble(0, maxVal);
        }

        /// <summary>
        /// Returns a double ranging from 0, inclusive, to 1, exclusive.
        /// </summary>
        /// <returns>An integer ranging from 0 to 1.</returns>
        public static double GetRandomDouble()
        {
            return RNGNum.NextDouble();
        }

        public static T GetRandomValue<T>(this IEnumerable<T> values)
        {
            int length = values.Count();

            List<T> valuesList = values.ToList();

            return valuesList[GetRandomInteger(length)];
        }
    }
}

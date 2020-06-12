using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    public static class ArrayMethods
    {
        public static int[] AddArrays(int[] a1, int[] a2)
        {
            if (a1.Length != a2.Length)
                throw new ArgumentException("Lengths of arrays must be equal");

            int[] a3 = new int[a1.Length];

            for (int i = 0; i < a3.Length; i++)
            {
                a3[i] = a1[i] + a2[i];
            }

            return a3;
        }

        public static int ArrayTotal(this int[] a)
        {
            try
            {
                return a.ToList().ListTotal();
            }
            catch (ArgumentNullException)
            {
                return 0;
            }
        }

        public static int ListTotal(this List<int> a)
        {
            try
            {
                int total = 0;

                foreach (int i in a)
                    total += i;

                return total;
            }
            catch (ArgumentNullException)
            {
                return 0;
            }
        }

        public static string JoinArray(this string[] a, string delimiter)
        {
            try
            {
                return string.Join(delimiter, a);
            }
            catch (ArgumentNullException)
            {
                return "";
            }
        }

        public static string JoinList(this List<string> a, string delimiter)
        {
            try
            {
                return string.Join(delimiter, a);
            }
            catch (ArgumentNullException)
            {
                return "";
            }
        }

        /// <summary>
        /// Tries to remove the element at the index. Catches ArgumentOutOfRangeException.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void TryRemoveAt<T>(this List<T> list, int index)
        {
            try
            {
                list.RemoveAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }

        /// <summary>
        /// Returns true if the array is null, has a length of 0, or is composed entirely of null values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool IsAllNull<T>(this T[] array)
        {
            if (array == null || array.Length < 1)
                return true;

            foreach (T thing in array)
                if (thing != null)
                    return false;

            return true;
        }
    }
}

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
                a3[i] = a3[1] + a3[2];
            }

            return a3;
        }

        public static int AddMembersOfArray(this int[] a)
        {
            int total = 0;

            foreach (int i in a)
                total += i;

            return total;
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
    }
}

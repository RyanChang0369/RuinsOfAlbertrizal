using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    public static class RoundKeeper
    {
        public static void TurnStart<T>(List<T> list) where T : IRoundBasedObject
        {
            TurnStart(list.ToArray());
        }

        public static void TurnStart<T>(T[] array) where T : IRoundBasedObject
        {
            foreach (T single in array)
            {
                if (single != null)
                    TurnStart(single);
            }
        }

        public static void TurnStart<T>(T single) where T : IRoundBasedObject
        {
            single.StartTurn();
        }

        public static void TurnEnd<T>(List<T> list) where T : IRoundBasedObject
        {
            TurnEnd(list.ToArray());
        }

        public static void TurnEnd<T>(T[] array) where T : IRoundBasedObject
        {
            foreach (T single in array)
            {
                if (single != null)
                    TurnEnd(single);
            }
        }

        public static void TurnEnd<T>(T single) where T : IRoundBasedObject
        {
            single.EndTurn();
        }

        public static void RoundStart<T>(List<T> list) where T : IRoundBasedObject
        {
            RoundStart(list.ToArray());
        }

        public static void RoundStart<T>(T[] array) where T : IRoundBasedObject
        {
            foreach (T single in array)
            {
                if (single != null)
                    RoundStart(single);
            }
        }

        /// <summary>
        /// Starts the round and turn
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="single"></param>
        public static void RoundStart<T>(T single) where T : IRoundBasedObject
        {
            single.StartRound();
            single.StartTurn();
        }

        public static void RoundEnd<T>(List<T> list) where T : IRoundBasedObject
        {
            RoundEnd(list.ToArray());
        }

        public static void RoundEnd<T>(T[] array) where T : IRoundBasedObject
        {
            foreach (T single in array)
            {
                if (single != null)
                    RoundEnd(single);
            }
        }

        /// <summary>
        /// Ends the round and turn
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="single"></param>
        public static void RoundEnd<T>(T single) where T : IRoundBasedObject
        {
            single.EndTurn();
            single.EndRound();
        }
    }
}

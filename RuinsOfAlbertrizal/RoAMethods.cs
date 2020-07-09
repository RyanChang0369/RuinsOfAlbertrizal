using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal
{
    public static class RoAMethods
    {
        public static List<Guid> ToGlobalIDList<T>(this List<T> objects) where T : ObjectOfAlbertrizal
        {
            List<Guid> guids = new List<Guid>();

            foreach (T thing in objects)
                guids.Add(thing.GlobalID);

            return guids;
        }

        public static List<T> FilterByGlobalID<T>(this List<T> objects, List<Guid> guids) where T : ObjectOfAlbertrizal
        {
            List<T> filteredList = new List<T>();

            foreach (T thing in objects)
            {
                if (guids.Contains(thing.GlobalID))
                    filteredList.Add(thing);
            }

            return filteredList;
        }

        /// <summary>
        /// Filters an array of objects. The location of objects is preserved. Empty Guids will be interpreted as null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects"></param>
        /// <param name="guids"></param>
        /// <returns></returns>
        public static T[] FilterByGlobalID<T>(this T[] objects, Guid[] guids) where T : ObjectOfAlbertrizal
        {
            T[] filteredArr = new T[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                if (guids[i] == null || guids[i].Equals(Guid.Empty))
                {
                    continue;
                }

                foreach (T thing in objects)
                {
                    if (thing.GlobalID.Equals(guids[i]))
                        filteredArr[i] = thing;
                }
            }

            return filteredArr;
        }

        public static List<T> FilterByInstanceID<T>(this List<T> objects, List<Guid> guids) where T : ObjectOfAlbertrizal
        {
            List<T> filteredList = new List<T>();

            foreach (T thing in objects)
            {
                if (guids.Contains(thing.InstanceID))
                    filteredList.Add(thing);
            }

            return filteredList;
        }

        /// <summary>
        /// Filters an array of objects. The location of objects is preserved. Empty Guids will be interpreted as null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects"></param>
        /// <param name="guids"></param>
        /// <returns></returns>
        public static T[] FilterByInstanceID<T>(this T[] objects, Guid[] guids) where T : ObjectOfAlbertrizal
        {
            T[] filteredArr = new T[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                if (guids[i] == null || guids[i].Equals(Guid.Empty))
                {
                    continue;
                }

                foreach (T thing in objects)
                {
                    if (thing.InstanceID.Equals(guids[i]))
                        filteredArr[i] = thing;
                }
            }

            return filteredArr;
        }

        /// <summary>
        /// Uses StaticMap to clone a single ObjectOfAlbertrizal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectOfAlbertrizal">An object from static map.</param>
        /// <returns></returns>
        public static T StaticMapClone<T>(this T objectOfAlbertrizal) where T : ObjectOfAlbertrizal
        {
            if (!GameBase.Initialized())
                throw new Exception("Both static and current maps must be initialized");

            //Break link
            GameBase.StaticGame = FileHandler.LoadMap(GameBase.StaticMapLocation);
            objectOfAlbertrizal.GetNewInstanceID();

            return objectOfAlbertrizal;
        }

        /// <summary>
        /// Uses StaticMap to clone an entire list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> StaticMapClone<T>(this List<T> listOfObjects) where T : ObjectOfAlbertrizal
        {
            if (!GameBase.Initialized())
                throw new Exception("Both static and current maps must be initialized");

            GameBase.StaticGame = FileHandler.LoadMap(GameBase.StaticMapLocation);

            return listOfObjects;
        }

        /// <summary>
        /// Clones an ObjectOfAlbertrizal and gives it a new InstanceID.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thing">Any Xml serializable object</param>
        /// <returns></returns>
        public static T RoAMemoryClone<T>(this T thing) where T : ObjectOfAlbertrizal
        {
            thing = MiscMethods.MemoryClone(thing);
            thing.GetNewInstanceID();
            return thing;
        }

        public static int TotalBI<T>(this IEnumerable<T> characters) where T : Character
        {
            int total = 0;
            foreach (T character in characters)
            {
                try
                {
                    total += character.BattleIndex;
                }
                catch (NullReferenceException)
                {

                }
            }
            return total;
        }

        public static int AverageBI<T>(this IEnumerable<T> characters, bool includeNull) where T : Character
        {
            if (includeNull)
            {
                return TotalBI(characters) / characters.Count();
            }
            else
            {
                int total = 0;
                int number = 0;
                foreach (T character in characters)
                {
                    try
                    {
                        total += character.BattleIndex;
                        number++;
                    }
                    catch (NullReferenceException)
                    {

                    }
                }
                return total / number;
            }
        }
    }
}

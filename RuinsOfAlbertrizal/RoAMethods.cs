using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal
{
    public static class RoAMethods
    {
        public static List<Guid> ToGuidList<T>(this List<T> objects) where T : ObjectOfAlbertrizal
        {
            return ObjectOfAlbertrizal.ToGuidList(objects);
        }

        public static List<T> FilterByGuid<T>(this List<T> objects, List<Guid> guids) where T : ObjectOfAlbertrizal
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
        public static T[] FilterByGuid<T>(this T[] objects, Guid[] guids) where T : ObjectOfAlbertrizal
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
        /// Clones an object without needing a static map.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectOfAlbertrizal">Any ObjectOfAlbertrizal</param>
        /// <returns></returns>
        public static T MemoryClone<T>(this T objectOfAlbertrizal) where T : ObjectOfAlbertrizal
        {
            XmlSerializer serializer = new XmlSerializer(objectOfAlbertrizal.GetType());
            MemoryStream memoryStream = new MemoryStream();
            serializer.Serialize(memoryStream, objectOfAlbertrizal);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return (T)serializer.Deserialize(memoryStream);
        }
    }
}

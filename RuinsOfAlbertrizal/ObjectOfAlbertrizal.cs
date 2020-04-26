using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Everything must have a name and description in this world.
    /// </summary>
    public abstract class ObjectOfAlbertrizal
    {
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Gets the names of all members of a list of ObjectsOfAlbertrizal
        /// </summary>
        /// <param name="objectsOfAlbertrizal"></param>
        /// <returns></returns>
        public static string[] GetNames(List<ObjectOfAlbertrizal> objectsOfAlbertrizal)
        {
            string[] names = new string[objectsOfAlbertrizal.Count];

            for (int i = 0; i < objectsOfAlbertrizal.Count; i++)
            {
                names[i] = objectsOfAlbertrizal[i].Name;
            }

            return names;
        }

        /// <summary>
        /// Gets the index of the ObjectOfAlberizal by name, or returns -1 if list does not contain an object with such name.
        /// </summary>
        /// <param name="objectsOfAlbertrizal"></param>
        /// <param name="objectOfAlbertrizal"></param>
        /// <returns></returns>
        public static int GetIndexOfName(List<ObjectOfAlbertrizal> objectsOfAlbertrizal, ObjectOfAlbertrizal objectOfAlbertrizal)
        {
            string[] names = GetNames(objectsOfAlbertrizal);

            for (int i = 0; i < objectsOfAlbertrizal.Count; i++)
            {
                if (objectsOfAlbertrizal[i].Name == objectOfAlbertrizal.Name)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}

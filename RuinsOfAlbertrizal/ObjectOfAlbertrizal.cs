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
    }
}

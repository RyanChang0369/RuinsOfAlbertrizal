using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

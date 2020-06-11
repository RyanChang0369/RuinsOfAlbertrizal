using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    public static class RoAMethods
    {
        public static List<Guid> ToGuidList(this List<ObjectOfAlbertrizal> objects)
        {
            return ObjectOfAlbertrizal.ToGuidList(objects);
        }

        public static List<ObjectOfAlbertrizal> FilterByGuid(this List<ObjectOfAlbertrizal> objects, List<Guid> guids)
        {
            List<ObjectOfAlbertrizal> filteredList = new List<ObjectOfAlbertrizal>();

            foreach (ObjectOfAlbertrizal thing in objects)
            {
                if (guids.Contains(thing.GlobalID))
                    filteredList.Add(thing);
            }

            return filteredList;
        }
    }
}

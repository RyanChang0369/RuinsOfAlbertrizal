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
    }
}

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
    }
}

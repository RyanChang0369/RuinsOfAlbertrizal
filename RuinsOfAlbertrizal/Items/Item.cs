using RuinsOfAlbertrizal.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Items
{
    public class Item : IconedObjectOfAlbertrizal
    {
        /// <summary>
        /// Percent chance this item will drop from an enemy, ranging from 0.0 to 1.0
        /// </summary>
        public double DropChance { get; set; }

        public Item()
        { }
    }
}

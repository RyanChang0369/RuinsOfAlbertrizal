using RuinsOfAlbertrizal.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Items
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rarity { get; set; }
        public List<Enemy> DroppedBy { get; set; }

        /// <summary>
        /// Create a new item
        /// </summary>
        /// <param name="name">The name of this item.</param>
        /// <param name="description">The text to display below the name.</param>
        /// <param name="rarity">The percent chance of finding this item per enemy killed.</param>
        /// <param name="droppedBy">Which enemies drop this item.</param>
        public Item(string name, string description, int rarity, List<Enemy> droppedBy)
        {
            Name = name;
            Description = description;
        }
    }
}

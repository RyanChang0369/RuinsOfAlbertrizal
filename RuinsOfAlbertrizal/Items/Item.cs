using System;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Items
{
    
    public class Item : IconedObjectOfAlbertrizal
    {
        public static Predicate<Item> IsDefault => item => item.NumGivenOnStart > 0;

        /// <summary>
        /// Percent chance this item will drop from an enemy, ranging from 0.0 to 1.0
        /// </summary>
        public double DropChance { get; set; }

        public int NumGivenOnStart { get; set; }

        public Item()
        { }
    }
}

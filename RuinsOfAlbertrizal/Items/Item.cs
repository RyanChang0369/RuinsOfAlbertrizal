using System;

namespace RuinsOfAlbertrizal.Items
{
    [Serializable]
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

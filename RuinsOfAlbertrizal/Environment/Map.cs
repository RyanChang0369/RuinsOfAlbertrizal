using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System.Collections.Generic;

namespace RuinsOfAlbertrizal.Environment
{
    public class Map
    {
        public List<string> IntroText { get; set; }

        /// <summary>
        /// Number of enemies to kill before boss fight. Set to -1 for unwinnable
        /// </summary>
        public int EnemiesToKill { get; set; }
        
        public Boss Boss { get; set; }

        public Player Player { get; set; }

        public List<Enemy> Enemies { get; set; }

        public List<Buff> Buffs { get; set; }

        public List<Item> Items { get; set; }

        public List<Consumable> Consumables { get; set; }

        public List<Equiptment> Equiptments { get; set; }
    }
}
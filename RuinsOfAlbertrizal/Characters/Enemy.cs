using RuinsOfAlbertrizal.AIs;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Characters
{
    [Serializable]
    public class Enemy : Character
    {
        public List<Equiptment> InventoryEquiptments { get; set; }

        public List<Consumable> InventoryConsumables { get; set; }

        public List<Item> InventoryItems { get; set; }

        /// <summary>
        /// A message is selected randomly from this list. Which enemy plays this message is determined by its Battle Index.
        /// </summary>
        public List<Message> MessagesOnEncounter { get; set; }

        /// <summary>
        /// A message is selected randomly from this list. Which enemy plays this message is determined by its Battle Index.
        /// </summary>
        public List<Message> MessagesOnAttack { get; set; }

        /// <summary>
        /// A message is selected randomly from this list. Which enemy plays this message is determined by its Battle Index.
        /// </summary>
        public List<Message> MessagesOnDefeat { get; set; }

        /// <summary>
        /// A message is selected randomly from this list. Which enemy plays this message is determined by its Battle Index.
        /// </summary>
        public List<Message> MessagesOnVictory { get; set; }

        private AI.AIStyle aiStyle;

        public override AI.AIStyle AIStyle
        {
            get => aiStyle;
            set
            {
                if (value == AI.AIStyle.NoChange)
                    return;
                else if (value == AI.AIStyle.Player)
                    aiStyle = AI.AIStyle.NoAI;

                aiStyle = value;
            }
        }

        public Enemy() : base()
        {
            AIStyle = AI.AIStyle.NoAI;
            MessagesOnAttack = new List<Message>();
            MessagesOnDefeat = new List<Message>();
            MessagesOnEncounter = new List<Message>();
            MessagesOnVictory = new List<Message>();
            InventoryEquiptments = new List<Equiptment>();
            CurrentConsumables = new List<Consumable>();
            InventoryConsumables = new List<Consumable>();
            InventoryItems = new List<Item>();
        }

        /// <summary>
        /// Use this to get the amount of XP gained from killing this enemy.
        /// </summary>
        /// <returns></returns>
        public int GetXPGained()
        {
            return (int)Math.Round(BattleIndex / 10.0);
        }

        /// <summary>
        /// Use this to get the amount of level points gained from killing this enemy
        /// </summary>
        /// <returns></returns>
        public int GetPointsGained()
        {
            return GetXPGained();
        }

        public override void Die()
        {
            
        }
    }
}

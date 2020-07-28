using RuinsOfAlbertrizal.AIs;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Characters
{
    
    public class Enemy : Character
    {
        public List<Guid> InventoryEquiptmentGuids { get; set; }

        /// <summary>
        /// Serves as the loot table
        /// </summary>
        [XmlIgnore]
        public List<Equiptment> InventoryEquiptments { get; set; }

        public List<Guid> InventoryConsumableGuids { get; set; }

        /// <summary>
        /// Serves as the loot table. Enemies can also use these items.
        /// </summary>
        [XmlIgnore]
        public List<Consumable> InventoryConsumables { get; set; }

        public List<Guid> InventoryItemGuids { get; set; }

        /// <summary>
        /// Serves as the loot table
        /// </summary>
        [XmlIgnore]
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

        public double SpawnChance { get; set; }

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
            SpawnChance = 1.0;
            MessagesOnAttack = new List<Message>();
            MessagesOnDefeat = new List<Message>();
            MessagesOnEncounter = new List<Message>();
            MessagesOnVictory = new List<Message>();
            InventoryEquiptments = new List<Equiptment>();
            CurrentConsumables = new List<Consumable>();
            InventoryConsumables = new List<Consumable>();
            InventoryItems = new List<Item>();
        }

        public override void Load(Map map)
        {
            base.Load(map);
            InventoryConsumables = map.StoredConsumables.FilterByGlobalID(InventoryConsumableGuids);
            InventoryEquiptments = map.StoredEquiptments.FilterByGlobalID(InventoryEquiptmentGuids);
            InventoryItems = map.StoredItems.FilterByGlobalID(InventoryItemGuids);
        }

        public override void Unload(bool force)
        {
            base.Unload(force);

            InventoryConsumableGuids = InventoryConsumables.ToGlobalIDList();

            if (force)
            {
                InventoryEquiptmentGuids = InventoryEquiptments.ToGlobalIDList();
                InventoryItemGuids = InventoryItems.ToGlobalIDList();
            }
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

        public override void Consume(Consumable consumable)
        {
            CurrentConsumables.Add(consumable);
            InventoryConsumables.Remove(consumable);
            GameBase.CurrentGame.CurrentBattleField.NotifyItemUsed(consumable, this);
        }

        public void Run(BattleField battleField)
        {
            int avePlayerSpd = 0;

            foreach (Player player in battleField.AlivePlayers)
            {
                avePlayerSpd += player.CurrentStats[4];
            }

            if (CurrentStats[4] > avePlayerSpd)
            {
                battleField.ActiveEnemies[Array.IndexOf(battleField.ActiveEnemies, this)] = null;
                battleField.Enemies.Remove(this);
                battleField.StoredMessage.Add($"Enemy {DisplayName} ran away!");
            }
            else
                battleField.StoredMessage.Add($"Enemy {DisplayName} failed to run away!");
        }
    }
}

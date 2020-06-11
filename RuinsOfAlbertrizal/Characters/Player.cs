using RuinsOfAlbertrizal.AIs;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Characters
{
    
    public class Player : Character, INotifyPropertyChanged
    {
        [XmlIgnore]
        public List<Item> InventoryItems
        {
            get => GameBase.CurrentGame.PlayerItems;
            set
            {
                GameBase.CurrentGame.PlayerItems = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        public List<Equiptment> InventoryEquiptments
        {
            get => GameBase.CurrentGame.PlayerEquiptments;
            set
            {
                GameBase.CurrentGame.PlayerEquiptments = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        public List<Consumable> InventoryConsumables
        {
            get => GameBase.CurrentGame.PlayerConsumables;
            set
            {
                GameBase.CurrentGame.PlayerConsumables = value;
                OnPropertyChanged();
            }
        }

        public int XP { get; set; }

        public Player() : base()
        {
            XP = 0;
            AIStyle = AI.AIStyle.Player;
        }

        public override void Die()
        {
            MessageBox.Show("You have died...");
            throw new NotImplementedException();
        }

        public static void Run(BattleField battleField)
        {
            int aveEnemySpd = 0;
            int avePlayerSpd = 0;

            foreach (Enemy enemy in battleField.AliveEnemies)
            {
                aveEnemySpd += enemy.CurrentStats[4];
            }

            foreach (Player player in battleField.Players)
            {
                //If player is dead, their speed is quartered
                if (player.IsDead)
                    avePlayerSpd += player.CurrentStats[4] / 4;
                else
                    avePlayerSpd += player.CurrentStats[4];
            }

            //Find the averages of speed
            aveEnemySpd /= battleField.AliveEnemies.Count;
            avePlayerSpd /= battleField.Players.Count;

            if (avePlayerSpd > aveEnemySpd) //If player speed is greater than enemy speed, then run success
                battleField.PlayerRunsAway();
            else if (GameBase.CurrentGame.TotalDifficulty <= 0) //Go easy on noobs
                battleField.PlayerRunsAway();
            else
            {
                int fateSelector = RNG.GetRandomPercent() - (int)Math.Round(GameBase.CurrentGame.TotalDifficulty * 10.0);
                int spdDiff = aveEnemySpd - avePlayerSpd;

                if (fateSelector > spdDiff)
                    battleField.PlayerRunsAway();
            }
        }

        /// <summary>
        /// Prompts the user to keep or discard the item. Creates a deep clone of the item.
        /// </summary>
        /// <param name="item"></param>
        public void ObtainItem(Item item)
        {
            IconedObjectPrompt prompt = new IconedObjectPrompt("You found an item!", GetItemFindMessage(item), item, "Keep", "Discard");

            if ((bool)prompt.DialogResult)
            {
                GameBase.CurrentGame.PlayerItems.Add(item);
            }
        }

        /// <summary>
        /// Prompts the user to keep or discard the equiptment. Creates a deep clone of the equiptment.
        /// </summary>
        /// <param name="equiptment"></param>
        public void ObtainEquiptment(Equiptment equiptment)
        {
            IconedObjectPrompt prompt = new IconedObjectPrompt("You found an equiptment!", GetItemFindMessage(equiptment), equiptment, "Keep", "Discard");

            if ((bool)prompt.DialogResult)
            {
                GameBase.CurrentGame.PlayerEquiptments.Add(equiptment);
            }
        }

        /// <summary>
        /// Prompts the user to keep or discard the consumable. Creates a deep clone of the consumable.
        /// </summary>
        /// <param name="consumable"></param>
        public void ObtainConsumable(Consumable consumable)
        {
            IconedObjectPrompt prompt = new IconedObjectPrompt("You found a consumable!", GetItemFindMessage(consumable), consumable, "Keep", "Discard");

            if ((bool)prompt.DialogResult)
            {
                GameBase.CurrentGame.PlayerConsumables.Add(consumable);
            }
        }

        private string GetItemFindMessage(Item item)
        {
            return $"Out of the corner of your eye, you spot a {item.Name}.";
        }

        public override void Consume(Consumable consumable)
        {
            CurrentConsumables.Add(consumable);
            GameBase.CurrentGame.PlayerConsumables.Remove(consumable);
            MessageBox.Show($"You ingested the {consumable.DisplayName}");
            GameBase.CurrentGame.CurrentBattleField.NotifyItemUsed(consumable, this);
        }

        /// <summary>
        /// Equipts an equiptable
        /// </summary>
        /// <param name="index">The index of the item in InventoryEquiptments</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Equipt(Equiptment equiptment)
        {
            //Unequipt all slots that this new equiptment will take up
            foreach (Equiptment.SlotMode slotMode in equiptment.Slots)
            {
                Unequipt((int)slotMode - 1);
            }

            foreach (Equiptment.SlotMode slotMode in equiptment.Slots)
            {
                CurrentEquiptments[(int)slotMode - 1] = equiptment;
            }

            InventoryEquiptments.Remove(equiptment);
            GameBase.CurrentGame.CurrentBattleField.NotifyItemUsed(equiptment, this);
        }

        /// <summary>
        /// Removes this equiptment and any of the slots it may have occupied.
        /// </summary>
        /// <param name="index"></param>
        public void Unequipt(int index)
        {
            if (CurrentEquiptments[index] == null)
                return;

            Equiptment equiptment = CurrentEquiptments[index];

            foreach (Equiptment.SlotMode slotMode in CurrentEquiptments[index].Slots)
            {
                CurrentEquiptments[(int)slotMode - 1] = null;
            }
            GameBase.CurrentGame.PlayerEquiptments.Add(equiptment);
        }

        /// <summary>
        /// Removes this equiptment and any of the slots it may have occupied. Throws ArgumentException if 
        /// equiptment is not found within CurrentEquiptments.
        /// </summary>
        /// <param name="equiptment"></param>
        /// <exception cref="ArgumentException"></exception>
        public void Unequipt(Equiptment equiptment)
        {
            if (!CurrentEquiptments.Contains(equiptment))
                throw new ArgumentException($"Equiptment {equiptment.Name} not found within CurrentEquiptments");

            Unequipt(Array.IndexOf(CurrentEquiptments, equiptment));
        }
    }
}

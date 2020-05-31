using RuinsOfAlbertrizal.AIs;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Characters
{
    [Serializable]
    public class Player : Character
    {
        [XmlIgnore]
        public List<Item> InventoryItems
        {
            get => GameBase.CurrentGame.PlayerItems;
            set => GameBase.CurrentGame.PlayerItems = value;
        }

        [XmlIgnore]
        public List<Equiptment> InventoryEquiptments
        {
            get => GameBase.CurrentGame.PlayerEquiptments;
            set => GameBase.CurrentGame.PlayerEquiptments = value;
        }

        [XmlIgnore]
        public List<Consumable> InventoryConsumables
        {
            get => GameBase.CurrentGame.PlayerConsumables;
            set => GameBase.CurrentGame.PlayerConsumables = value;
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

        /// <summary>
        /// Prompts the user to keep or discard the item. Creates a deep clone of the item.
        /// </summary>
        /// <param name="item"></param>
        public void ObtainItem(Item item)
        {
            IconedObjectPrompt prompt = new IconedObjectPrompt("You found an item!", GetItemFindMessage(item), item, "Keep", "Discard");

            if ((bool)prompt.DialogResult)
            {
                GameBase.CurrentGame.PlayerInventory.Add(item.DeepClone());
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
                GameBase.CurrentGame.PlayerInventory.Add(equiptment.DeepClone());
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
                GameBase.CurrentGame.PlayerInventory.Add(consumable.DeepClone());
            }
        }

        private string GetItemFindMessage(Item item)
        {
            return $"Out of the corner of your eye, you spot a {item.Name}.";
        }
    }
}

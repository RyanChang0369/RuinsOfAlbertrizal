using RuinsOfAlbertrizal.AIs;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RuinsOfAlbertrizal.Characters
{
    public class Player : Character
    {
        public Player()
        {
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
            IconedObjectPrompt prompt = new IconedObjectPrompt("You found an item!", $"Out of the corner of your eye, you spot a {item.Name}", item, "Keep", "Discard");

            if ((bool)prompt.DialogResult)
            {
                InventoryItems.Add(item.DeepClone());
            }
        }

        /// <summary>
        /// Prompts the user to keep or discard the equiptment. Creates a deep clone of the equiptment.
        /// </summary>
        /// <param name="equiptment"></param>
        public void ObtainEquiptment(Equiptment equiptment)
        {
            IconedObjectPrompt prompt = new IconedObjectPrompt("You found an equiptment!", $"Out of the corner of your eye, you spot a {equiptment.Name}", equiptment, "Keep", "Discard");

            if ((bool)prompt.DialogResult)
            {
                InventoryItems.Add(equiptment.DeepClone());
            }
        }

        /// <summary>
        /// Prompts the user to keep or discard the consumable. Creates a deep clone of the consumable.
        /// </summary>
        /// <param name="consumable"></param>
        public void ObtainConsumable(Consumable consumable)
        {
            IconedObjectPrompt prompt = new IconedObjectPrompt("You found a consumable!", $"Out of the corner of your eye, you spot a {consumable.Name}", consumable, "Keep", "Discard");

            if ((bool)prompt.DialogResult)
            {
                InventoryItems.Add(consumable.DeepClone());
            }
        }
    }
}

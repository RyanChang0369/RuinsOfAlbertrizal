using RuinsOfAlbertrizal.AIs;
using RuinsOfAlbertrizal.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Characters
{
    public class Enemy : Character
    {
        /// <summary>
        /// The amount of percentage one gains from killing one enemy.
        /// Boss fight starts at next encounter if this is equal to or exceeds 100%.
        /// </summary>
        public double PointGainPerKill { get; set; }

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

        public Enemy()
        {
            AIStyle = AIs.AI.AIStyle.NoAI;
            MessagesOnAttack = new List<Message>();
            MessagesOnDefeat = new List<Message>();
            MessagesOnEncounter = new List<Message>();
            MessagesOnVictory = new List<Message>();
        }

        public override void Die()
        {
            throw new NotImplementedException("");
        }
    }
}

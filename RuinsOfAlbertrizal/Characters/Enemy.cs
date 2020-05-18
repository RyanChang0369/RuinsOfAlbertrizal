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

        /// <summary>
        /// Use this to get the amount of XP gained from killing this enemy.
        /// </summary>
        /// <returns></returns>
        public int GetXPGained()
        {
            return BattleIndex / 10;
        }

        public override void Die()
        {
            throw new NotImplementedException();
        }
    }
}

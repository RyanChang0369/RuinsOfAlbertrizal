using RuinsOfAlbertrizal.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Mechanics
{
    [Serializable]
    public class Attack : ObjectOfAlbertrizal, ITurnBasedObject
    {
        public List<Buff> Buffs { get; set; }

        public int[] StatLoss { get; set; }

        public int[] StatCostToUser { get; set; }

        public int CoolDown { get; set; }

        public int ChargeUp { get; set; }

        /// <summary>
        /// Turns since last attacking with this attack, or 0 if not applicable.
        /// </summary>
        public int TurnSinceAttacked { get; set; }

        /// <summary>
        /// Turns since charging an attack, or 0 if not applicable.
        /// </summary>
        public int TurnsSinceBeginCharge { get; set; }

        public bool IsMagic { get; set; }

        public Attack()
        {
            StatLoss = new int[GameBase.NumStats];
            StatCostToUser = new int[GameBase.NumStats];
            Buffs = new List<Buff>();
        }

        [XmlIgnore]
        public bool CanAttack
        {
            get
            {
                return (CoolDown <= TurnSinceAttacked) && IsCharged;
            }
        }

        [XmlIgnore]
        public bool IsCharged
        {
            get
            {
                return ChargeUp <= TurnsSinceBeginCharge;
            }
        }

        /// <summary>
        /// Starts the attack.
        /// </summary>
        /// <param name="attacker">The character doing the attacking.</param>
        /// <param name="target">The character being attacked.</param>
        public void BeginAttack(Character attacker, Character target)
        {
            if (CanAttack)
            {
                TurnSinceAttacked = 0;
                TurnsSinceBeginCharge = 0;

                for (int i = 0; i < StatCostToUser.Length; i++)
                {
                    attacker.AppliedStats[i] -= StatCostToUser[i];
                }

                target.GetAttacked(this);
            }
            else if (!IsCharged)
            {
                //Begin charge
                TurnsSinceBeginCharge++;
            }
        }

        /// <summary>
        /// Deals damage
        /// </summary>
        /// <param name="target"></param>
        public void DealStats(Character target)
        {
            for (int i = 0; i < StatLoss.Length; i++)
            {
                target.AppliedStats[i] -= StatLoss[i]; 
            }
        }

        public void DealBuffs(Character target)
        {
            foreach (Buff buff in Buffs)
            {
                target.AppliedBuffs.Add(buff);
            }
        }

        public void EndTurn()
        {
            TurnSinceAttacked++;
        }

        public void StartTurn()
        {
            
        }
    }
}

using RuinsOfAlbertrizal.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Mechanics
{
    public class Attack : ObjectOfAlbertrizal, ITurnBasedObject
    {
        public List<Buff> Buffs { get; set; }

        public int[] StatLoss { get; set; }

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
        /// <param name="character">The character being attacked</param>
        public void BeginAttack(Character character)
        {
            if (CanAttack)
            {
                TurnSinceAttacked = 0;
                TurnsSinceBeginCharge = 0;

                character.GetAttacked(this);
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
        /// <param name="character"></param>
        public void DealStats(Character character)
        {
            for (int i = 0; i < StatLoss.Length; i++)
            {
                character.AppliedStats[i] -= StatLoss[i]; 
            }
        }

        public void DealBuffs(Character character)
        {
            foreach (Buff buff in Buffs)
            {
                character.AppliedBuffs.Add(buff);
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

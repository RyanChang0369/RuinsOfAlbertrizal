using RuinsOfAlbertrizal.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Mechanics
{
    public class Attack : ObjectOfAlbertrizal
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

        public Attack()
        {
            StatLoss = new int[5];
            Buffs = new List<Buff>();
        }

        [XmlIgnore]
        public bool CanAttack
        {
            get
            {
                return (CoolDown <= TurnSinceAttacked && IsCharged);
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
        public void BeginAttack(Character character)
        {
            if (CanAttack)
            {
                TurnSinceAttacked = 0;
                TurnsSinceBeginCharge = 0;

                for (int i = 0; i < StatLoss.Length; i++)
                {
                    character.AppliedStats[i] -= StatLoss[i];
                }
            }
            else if (!IsCharged)
            {
                TurnsSinceBeginCharge++;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Mechanics
{
    public class Attack
    {
        public Buff Buff { get; set; }

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

        public void BeginAttack()
        {
            if (CanAttack)
            {
                TurnSinceAttacked = 0;
                TurnsSinceBeginCharge = 0;

                //Deal damage
            }
            else if (!IsCharged)
            {
                TurnsSinceBeginCharge++;
            }
        }
    }
}

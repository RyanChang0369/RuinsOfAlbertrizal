using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Environment
{
    public class Hazard : ObjectOfAlbertrizal
    {
        public int[] StatLoss { get; set; }

        public List<Buff> Buffs { get; set; }

        public enum DamageDirection
        {
            /// <summary>
            /// No damage
            /// </summary>
            [Description("No damage")]
            None,
            /// <summary>
            /// Damages on all surfaces, including the inside.
            /// </summary>
            [Description("Damages on all surfaces, including the inside.")]
            All,
            /// <summary>
            /// Damages when character is inside this hazard
            /// </summary>
            [Description("Damages when character is inside this hazard")]
            Inside,
            /// <summary>
            /// Damages when character is on top of this harzard.
            /// </summary>
            [Description("Damages when character is on top of this harzard.")]
            Top,
            /// <summary>
            /// Damages when character is directly below this hazard.
            /// </summary>
            [Description("Damages when character is directly below this hazard.")]
            Bottom,
            /// <summary>
            /// Damages when character is directly left of this hazard.
            /// </summary>
            [Description("Damages when character is directly left of this hazard.")]
            Left,
            /// <summary>
            /// Damages when character is directly right of this hazard.
            /// </summary>
            [Description("Damages when character is directly right of this hazard.")]
            Right
        }

        public DamageDirection DirectionOfDamage { get; set; }

        public enum HazardType
        {
            /// <summary>
            /// Characters appear behind this hazard and can stand on it. 
            /// </summary>
            [Description("Characters appear behind this hazard and can stand on it.")]
            TangableBlock,
            /// <summary>
            /// Characters appear ahead of this hazard and can stand on it.
            /// </summary>
            [Description("Characters appear ahead of this hazard and can stand on it.")]
            TangableWall,
            /// <summary>
            /// Characters appear behind this hazard and cannot stand on it. 
            /// </summary>
            [Description("Characters appear behind this hazard and cannot stand on it.")]
            IntangableBlock,
            /// <summary>
            /// Characters appear ahead of this hazard and cannot stand on it.
            /// </summary>
            [Description("Characters appear ahead of this hazard and cannot stand on it.")]
            IntangableWall
        }

        public HazardType TypeOfHazard { get; set; }

        public Hazard()
        {
            DirectionOfDamage = new DamageDirection();
            TypeOfHazard = new HazardType();
        }
    }
}

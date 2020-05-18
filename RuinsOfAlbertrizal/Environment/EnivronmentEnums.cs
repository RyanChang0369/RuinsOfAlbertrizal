using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Environment
{
    public enum BlockType
    {
        /// <summary>
        /// Characters appear behind this hazard and can stand on it. 
        /// </summary>
        [Display(Name = "Tangable Block", Description = "Characters appear behind this hazard and can stand on it.")]
        TangableBlock,
        /// <summary>
        /// Characters appear ahead of this hazard and can stand on it.
        /// </summary>
        [Display(Name = "Tangable Wall", Description = "Characters appear ahead of this hazard and can stand on it.")]
        TangableWall,
        /// <summary>
        /// Characters appear behind this hazard and cannot stand on it. 
        /// </summary>
        [Display(Name = "Intangable Block", Description = "Characters appear behind this hazard and cannot stand on it.")]
        IntangableBlock,
        /// <summary>
        /// Characters appear ahead of this hazard and cannot stand on it.
        /// </summary>
        [Display(Name = "Intangable Wall", Description = "Characters appear ahead of this hazard and cannot stand on it.")]
        IntangableWall
    }
    public enum DamageDirection
    {
        /// <summary>
        /// No damage
        /// </summary>
        [Display(Name = "Test1")]
        [Description("No damage")]
        None,
        /// <summary>
        /// Damages on all surfaces, including the inside.
        /// </summary>
        [Display(Name = "Test2")]
        [Description("Damages on all surfaces, including the inside.")]
        All,
        /// <summary>
        /// Damages when character is inside this hazard
        /// </summary>
        [Display(Name = "Test3")]
        [Description("Damages when character is inside this hazard")]
        Inside,
        /// <summary>
        /// Damages when character is on top of this harzard.
        /// </summary>
        [Display(Name = "Test4")]
        [Description("Damages when character is on top of this harzard.")]
        Top,
        /// <summary>
        /// Damages when character is directly below this hazard.
        /// </summary>
        [Display(Name = "Test5")]
        [Description("Damages when character is directly below this hazard.")]
        Bottom,
        /// <summary>
        /// Damages when character is directly left of this hazard.
        /// </summary>
        [Display(Name = "Test6")]
        [Description("Damages when character is directly left of this hazard.")]
        Left,
        /// <summary>
        /// Damages when character is directly right of this hazard.
        /// </summary>
        [Display(Name = "Test7")]
        [Description("Damages when character is directly right of this hazard.")]
        Right
    }

    class EnivronmentEnums
    {
    }
}

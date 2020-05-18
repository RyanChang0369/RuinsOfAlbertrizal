using RuinsOfAlbertrizal.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Use this when an object needs to respond to a hard-coded buff effect
    /// </summary>
    public interface IUniqueBuffEffects
    {
        /// <summary>
        /// Kills the character (deals 10x character's current health)
        /// </summary>
        void InstaKill();

        /// <summary>
        /// Respawns character with 20% leveled health
        /// </summary>
        void Revive();

        /// <summary>
        /// Removes all applied buffs. Static buffs unaffected
        /// </summary>
        void Cleanse();
    }
}

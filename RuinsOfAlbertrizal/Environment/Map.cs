using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System.Collections.Generic;

namespace RuinsOfAlbertrizal.Environment
{
    public class Map
    {
        public List<string> IntroText { get; set; }

        public Player Player { get; set; }

        public List<Level> Levels { get; set; }


        public Map(List<string> introText, Player player, List<Level> levels)
        {
            IntroText = introText;
            Player = player;
            Levels = levels;
        }


    }
}
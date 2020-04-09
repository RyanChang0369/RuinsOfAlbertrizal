using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Environment
{
    public class Map
    {
        public List<string> IntroText { get; set; }

        public Player Player { get; set; }

        public List<Level> Levels { get; set; }

        public int LevelsCompleted { get; set; }

        [XmlIgnore]
        public Level CurrentLevel { get => Levels[LevelsCompleted]; }

        public Map(List<string> introText, Player player, List<Level> levels)
        {
            IntroText = introText;
            Player = player;
            Levels = levels;
            LevelsCompleted = 0;
        }


    }
}
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    public class CampaignLoader
    {
        public static void NewCampaign()
        {
            GameBase.NewGame(FileHandler.LoadMap("UserCampaign/newMap.xml"));
        }

        public static void LoadCampaign()
        {
            GameBase.NewGame(FileHandler.LoadMap("UserCampaign/userMap.xml"));
        }
    }
}

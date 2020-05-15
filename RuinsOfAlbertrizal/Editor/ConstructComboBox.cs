using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RuinsOfAlbertrizal.Editor
{
    public static class ConstructComboBox
    {
        public static TextBlock[] ContructFromEnum(Enum enumeruration)
        {
            Array enums = Enum.GetValues(enumeruration.GetType());
            TextBlock[] textBlocks = new TextBlock[enums.Length];

            for (int i = 0; i < enums.Length; i++)
            {
                textBlocks[i].Text = (string)enums.GetValue(i);
                textBlocks[i].ToolTip = "Test";
            }

            return textBlocks;
        }
    }
}

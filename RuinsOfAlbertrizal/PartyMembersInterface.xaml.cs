using RuinsOfAlbertrizal.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for PartyMembersInterface.xaml
    /// </summary>
    public partial class PartyMembersInterface : Page
    {
        public PartyMembersInterface()
        {
            InitializeComponent();
        }

        public PartyMembersInterface(List<Player> players)
        {
            InitializeComponent();
            UpdatePartyMembersStackPanel(players);
        }

        private void UpdatePartyMembersStackPanel(List<Player> players)
        {
            PartyMembersStackPanel.Children.Clear();

            Brush[] statNumBrushes =
            {
                new SolidColorBrush(Colors.Red),
                new SolidColorBrush(Colors.Blue),
                new SolidColorBrush(Colors.SteelBlue),
                new SolidColorBrush(Colors.Maroon),
                new SolidColorBrush(Colors.Yellow)
            };

            ColumnDefinition columnDef = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };

            RowDefinition rowDef = new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star)
            };


            foreach (Player player in players)
            {
                Label[] statTitles = new Label[5];
                Label[] statNums = new Label[5];
                StackPanel[] statPanels = new StackPanel[5];

                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(columnDef);
                grid.ColumnDefinitions.Add(columnDef);
                grid.RowDefinitions.Add(rowDef);
                grid.RowDefinitions.Add(rowDef);
                grid.RowDefinitions.Add(rowDef);

                int col = 0;
                int row = 0;

                for (int i = 0; i < 5; i++)
                {
                    if (col == 2)
                    {
                        col = 0;
                        row++;
                    }

                    //Create statPanels
                    statTitles[i] = new Label
                    {
                        Content = GameBase.StatNames[i]
                    };

                    statNums[i] = new Label
                    {
                        Content = $"{player.ArmoredStats[i]}/{player.CurrentStats[i]}",
                        FontSize = 18,
                        Foreground = statNumBrushes[i]
                    };

                    statPanels[i] = new StackPanel();
                    statPanels[i].Children.Add(statTitles[i]);
                    statPanels[i].Children.Add(statNums[i]);

                    //Organize statPanels
                    col++;
                    grid.Children.Add(statPanels[i]);
                    statPanels[i].SetValue(Grid.RowProperty, row);
                    statPanels[i].SetValue(Grid.ColumnProperty, col);
                }

                //Create playerNameLabel and image
                Label playerNameLabel = new Label
                {
                    Content = player.Name
                };

                Image playerImage = new Image
                {
                    Source = player.WorldImg.ToBitmapSource()
                };

                //Shunt everything into a stackpanel
                StackPanel containingStackPanel = new StackPanel();
                containingStackPanel.Children.Add(playerNameLabel);
                containingStackPanel.Children.Add(playerImage);
                containingStackPanel.Children.Add(grid);

                PartyMembersStackPanel.Children.Add(containingStackPanel);
            }
        }
    }
}

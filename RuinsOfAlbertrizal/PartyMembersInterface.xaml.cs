using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for PartyMembersInterface.xaml
    /// </summary>
    public partial class PartyMembersInterface : BasePage
    {
        public static Player SelectedPlayer;

        public PartyMembersInterface()
        {
            InitializeComponent();
            UpdatePartyMembersStackPanel(GameBase.CurrentGame.Players);
            DataContext = GameBase.CurrentGame.Players;
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
                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());

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
                    Label statTitle = new Label
                    {
                        Content = GameBase.StatNames[i]
                    };

                    Label statNum = new Label
                    {
                        Content = $"{player.ArmoredStats[i]}/{player.CurrentStats[i]}",
                        FontSize = 18,
                        Foreground = statNumBrushes[i]
                    };

                    StackPanel statPanel = new StackPanel();
                    statPanel.Children.Add(statTitle);
                    statPanel.Children.Add(statNum);

                    //Organize statPanels
                    grid.Children.Add(statPanel);
                    statPanel.SetValue(Grid.RowProperty, row);
                    statPanel.SetValue(Grid.ColumnProperty, col);
                    col++;
                }

                //Create playerNameLabel and image
                Label playerNameLabel = new Label
                {
                    Content = player.Name,
                    FontSize = 28,
                    FontFamily = new FontFamily("Script MT Bold")
                };

                Label speciesNameLabel = new Label
                {
                    Content = player.GeneralName,
                    FontSize = 18
                };

                Image playerImage = new Image
                {
                    Source = player.WorldImg.ToBitmapSource()
                };

                ListBox debuffsListBox = new ListBox
                {
                    Style = (Style)Resources["inventoryListBox"],
                    Height = 300
                };

                foreach (Buff buff in player.PermanentBuffs)
                {
                    ListBoxItem item = new ListBoxItem
                    {
                        Content = buff.DisplayName,
                        Background = new SolidColorBrush(Colors.DarkSlateGray),
                        Foreground = new SolidColorBrush(Colors.Snow),
                        ToolTip = "Buffs or debuffs fixed to this player. These are not affected by buff immunities."
                    };

                    debuffsListBox.Items.Add(item);
                }

                foreach (Equiptment equiptment in player.CurrentEquiptments)
                {
                    if (equiptment == null || equiptment.IsAClone)
                        continue;

                    foreach (Buff buff in equiptment.GrantedBuffs)
                    {
                        bool playerIsImmune = false;

                        foreach (Buff immunity in player.AllBuffImmunities)
                        {
                            if (buff.HasSameGlobalIDAs(immunity))
                            {
                                playerIsImmune = true;
                                break;
                            }
                        }

                        if (playerIsImmune)
                            continue;

                        ListBoxItem item = new ListBoxItem
                        {
                            Content = buff.DisplayName,
                            Background = new SolidColorBrush(Colors.DimGray),
                            Foreground = new SolidColorBrush(Colors.Snow),
                            ToolTip = "Buffs or debuffs granted by current equiptment"
                        };

                        debuffsListBox.Items.Add(item);
                    }
                }

                foreach (Consumable consumable in player.CurrentConsumables)
                {
                    foreach (Buff buff in consumable.Buffs)
                    {
                        bool playerIsImmune = false;

                        foreach (Buff immunity in player.AllBuffImmunities)
                        {
                            if (buff.HasSameGlobalIDAs(immunity))
                            {
                                playerIsImmune = true;
                                break;
                            }
                        }

                        if (playerIsImmune)
                            continue;

                        ListBoxItem item = new ListBoxItem
                        {
                            Content = buff.DisplayName,
                            Background = new SolidColorBrush(Colors.DarkGray),
                            Foreground = new SolidColorBrush(Colors.Snow),
                            ToolTip = "Buffs or debuffs granted by consumables"
                        };

                        debuffsListBox.Items.Add(item);
                    }
                }

                foreach (Buff buff in player.AppliedBuffs)
                {
                    bool playerIsImmune = false;

                    foreach (Buff immunity in player.AllBuffImmunities)
                    {
                        if (buff.HasSameGlobalIDAs(immunity))
                        {
                            playerIsImmune = true;
                            break;
                        }
                    }

                    if (playerIsImmune)
                        continue;

                    ListBoxItem item = new ListBoxItem
                    {
                        Content = buff.DisplayName,
                        ToolTip = "Buffs or debuffs gained from being attacked or being buffed by a teammate"
                    };

                    debuffsListBox.Items.Add(item);
                }

                ScrollViewer buffScrollViewer = new ScrollViewer
                {
                    Content = debuffsListBox
                };

                Separator separator = new Separator
                {
                    MinHeight = 50,
                    Background = new SolidColorBrush(Colors.Transparent)
                };

                Label buffLabel = new Label
                {
                    Content = "Buffs and Debuffs"
                };

                Button inventoryBtn = new Button
                {
                    Content = "Inventory",
                    Style = (Style)Application.Current.FindResource("buttonSmallStretch"),
                    FontSize = 24,
                    Tag = player
                };

                inventoryBtn.Click += InventoryBtn_Click;

                Button setActiveBtn = new Button
                {
                    Style = (Style)Application.Current.FindResource("buttonSmallStretch"),
                    FontSize = 24,
                    Tag = player,
                };

                Label isActiveLbl = new Label
                {
                    FontWeight = FontWeights.UltraBold
                };

                if (!GameBase.CurrentGame.ActivePlayerGuids.Contains(player.GlobalID))
                {
                    setActiveBtn.Content = "Set as Active";
                    setActiveBtn.ToolTip = "Click to put this player on the front lines";
                    setActiveBtn.Click += SetActiveBtn_Click;
                    isActiveLbl.Content = "";
                }
                else
                {
                    setActiveBtn.Content = "Remove from Active";
                    setActiveBtn.ToolTip = "Click to remove this player from the front lines";
                    setActiveBtn.Click += SetActiveBtn_Click_Remove;
                    isActiveLbl.Content = "Currently Active";
                }

                //Shunt everything into a stackpanel
                StackPanel containingStackPanel = new StackPanel();
                containingStackPanel.Children.Add(playerNameLabel);
                containingStackPanel.Children.Add(playerImage);
                containingStackPanel.Children.Add(grid);
                containingStackPanel.Children.Add(separator);
                containingStackPanel.Children.Add(buffLabel);
                containingStackPanel.Children.Add(buffScrollViewer);
                containingStackPanel.Children.Add(inventoryBtn);
                containingStackPanel.Children.Add(setActiveBtn);
                containingStackPanel.Children.Add(isActiveLbl);

                PartyMembersStackPanel.Children.Add(containingStackPanel);
            }
        }

        private void InventoryBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            SelectedPlayer = (Player)btn.Tag;
            Navigate("InventoryInterface.xaml");
        }

        private void SetActiveBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            PartySlotSelector selector = new PartySlotSelector();
            selector.ShowDialog();
            int index = selector.SelectedIndex;

            if (index < 0)
                return;
            else
            {
                GameBase.CurrentGame.ActivePlayerGuids[index] = ((Player)btn.Tag).GlobalID;
                FileHandler.SaveCurrentMap();
                UpdatePartyMembersStackPanel(GameBase.CurrentGame.Players);
            }
        }

        private void SetActiveBtn_Click_Remove(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Array.IndexOf(GameBase.CurrentGame.ActivePlayerGuids, ((Player)btn.Tag).GlobalID);
            GameBase.CurrentGame.ActivePlayerGuids[index] = Guid.Empty;
            FileHandler.SaveCurrentMap();
            UpdatePartyMembersStackPanel(GameBase.CurrentGame.Players);
        }
    }
}

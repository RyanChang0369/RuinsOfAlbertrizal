using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Editor.AdderPrompts;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using RuinsOfAlbertrizal.XMLInterpreter;
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

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreateMapPrompt.xaml
    /// </summary>
    public partial class CreateMapPrompt : Page
    {
        public static Map Map { get; set; }

        public static int SelectedTab;

        public static bool DoNotUpdate;

        public static bool HasSaved = true;

        public bool[] StepsDone = new bool[13];

        public CreateMapPrompt()
        {
            InitializeComponent();

            if (GameBase.CurrentGame == null)
                Map = new Map();
            else
                Map = GameBase.CurrentGame;

            DataContext = Map;
            UpdateComponent();
        }

        private void UpdateComponent()
        {
            MainTabControl.SelectedIndex = SelectedTab;
            StatusBar.Content = "";

            if (DoNotUpdate)
            {
                DoNotUpdate = false;
                return;
            }

            //Step 1: Player
            if (CreatePlayerPrompt.CreatedPlayer != null)
            {
                if (!Map.StoredPlayers.Contains(CreatePlayerPrompt.CreatedPlayer))
                    Map.StoredPlayers.Add(CreatePlayerPrompt.CreatedPlayer);

                StepsDone[0] = true;
            }

            //Step 2: Enemies
            if (CreateEnemyPrompt.CreatedEnemy != null)
            {
                if (!Map.StoredEnemies.Contains(CreateEnemyPrompt.CreatedEnemy))
                    Map.StoredEnemies.Add(CreateEnemyPrompt.CreatedEnemy);

                StepsDone[1] = true;
            }

            //Step 3: Bosses
            if (CreateBossPrompt.CreatedBoss != null)
            {
                if (!Map.StoredBosses.Contains(CreateBossPrompt.CreatedBoss))
                    Map.StoredBosses.Add(CreateBossPrompt.CreatedBoss);

                StepsDone[2] = true;
            }

            //Step 4: Buffs
            if (CreateBuffPrompt.CreatedBuff != null)
            {
                if (!Map.StoredBuffs.Contains(CreateBuffPrompt.CreatedBuff))
                    Map.StoredBuffs.Add(CreateBuffPrompt.CreatedBuff);

                StepsDone[3] = true;
            }

            //Step 5: Attacks
            if (CreateAttackPrompt.CreatedAttack != null)
            {
                if (!Map.StoredAttacks.Contains(CreateAttackPrompt.CreatedAttack))
                    Map.StoredAttacks.Add(CreateAttackPrompt.CreatedAttack);

                StepsDone[4] = true;
            }

            //Step 6: Players
            if (CreatePlayerPrompt.CreatedPlayer != null)
            {
                if (!Map.StoredPlayers.Contains(CreatePlayerPrompt.CreatedPlayer))
                    Map.StoredPlayers.Add(CreatePlayerPrompt.CreatedPlayer);

                StepsDone[5] = true;
            }

            //Step 7: Blocks
            if (CreateBlockPrompt.CreatedBlock != null)
            {
                if (!Map.StoredBlocks.Contains(CreateBlockPrompt.CreatedBlock))
                    Map.StoredBlocks.Add(CreateBlockPrompt.CreatedBlock);

                StepsDone[6] = true;
            }

            //Step 8: Hazards
            if (CreateHazardPrompt.CreatedHazard != null)
            {
                if (!Map.StoredHazards.Contains(CreateHazardPrompt.CreatedHazard))
                    Map.StoredHazards.Add(CreateHazardPrompt.CreatedHazard);

                StepsDone[7] = true;
            }

            //Step 9: Items
            if (CreateItemPrompt.CreatedItem != null)
            {
                if (!Map.StoredItems.Contains(CreateItemPrompt.CreatedItem))
                    Map.StoredItems.Add(CreateItemPrompt.CreatedItem);

                StepsDone[8] = true;
            }

            //Step 10: Equiptment
            if (CreateEquiptmentPrompt.CreatedEquiptment != null)
            {
                if (!Map.StoredEquiptments.Contains(CreateEquiptmentPrompt.CreatedEquiptment))
                    Map.StoredEquiptments.Add(CreateEquiptmentPrompt.CreatedEquiptment);

                StepsDone[9] = true;
            }

            //Step 11: Level
            if (CreateLevelPrompt.CreatedLevel != null)
            {
                if (!Map.Levels.Contains(CreateLevelPrompt.CreatedLevel))
                    Map.Levels.Add(CreateLevelPrompt.CreatedLevel);

                StepsDone[10] = true;
            }

            //SaveBtn.IsEnabled = true;
            //GameBase.NewGame();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.RelativeOrAbsolute));
            DoNotUpdate = true;
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            StatusBar.Content = "Saving...";
            GameBase.CurrentGame = Map;
            FileHandler.SaveObject(typeof(Map), Map, GameBase.CustomMapLocation);
            
            StatusBar.Content = "Saved!";
            HasSaved = true;
        }
        private void Load(object sender, RoutedEventArgs e)
        {
            FileHandler.LoadCustomCampaign();
            Map = GameBase.CurrentGame;
        }

        /// <summary>
        /// Uses tag to navigate to correct editor page (relative uri).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Navigate(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            NavigationService.Navigate(new Uri((string)btn.Tag, UriKind.RelativeOrAbsolute));
            HasSaved = false;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            SelectedTab = MainTabControl.SelectedIndex;
        }



        private void ClearSelection(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel stackPanel = (StackPanel)VisualTreeHelper.GetParent(button);

            switch (stackPanel.Tag)
            {
                case "Enemy":
                    CreatedEnemiesList.SelectedIndex = -1;
                    CreateEnemyBtn.Content = "Create Enemy";
                    break;
                case "Boss":
                    CreatedBossesList.SelectedIndex = -1;
                    CreateBossBtn.Content = "Create Boss";
                    break;
                case "Buff":
                    CreatedBuffsList.SelectedIndex = -1;
                    CreateBuffBtn.Content = "Create Buff";
                    break;
                case "Attack":
                    CreatedAttacksList.SelectedIndex = -1;
                    CreateAttackBtn.Content = "Create Attack";
                    break;
                case "Player":
                    CreatedPlayersList.SelectedIndex = -1;
                    CreatePlayerBtn.Content = "Create Player";
                    break;
                case "Hazard":
                    CreatedHazardsList.SelectedIndex = -1;
                    CreateHazardBtn.Content = "Create Hazard";
                    break;
                case "Block":
                    CreatedBlocksList.SelectedIndex = -1;
                    CreateBlockBtn.Content = "Create Block";
                    break;
                case "Item":
                    CreatedItemsList.SelectedIndex = -1;
                    CreateItemBtn.Content = "Create Item";
                    break;
                case "Equiptment":
                    CreatedEquiptmentsList.SelectedIndex = -1;
                    CreateEquiptmentBtn.Content = "Create Equiptment";
                    break;
                case "Consumable":
                    CreatedConsumablesList.SelectedIndex = -1;
                    CreateConsumableBtn.Content = "Create Consumable";
                    break;
                case "Level":
                    CreatedLevelsList.SelectedIndex = -1;
                    CreatedLevelBtn.Content = "Create Consumable";
                    break;
                default:
                    throw new ArgumentException("Tag missing or invalid");
            }
        }

        private void DeleteSelection(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel stackPanel = (StackPanel)VisualTreeHelper.GetParent(button);

            try
            {
                switch (stackPanel.Tag)
                {
                    case "Enemy":
                        CreateEnemyPrompt.CreatedEnemy = new Enemy();
                        Map.StoredEnemies.RemoveAt(CreatedEnemiesList.SelectedIndex);
                        CreatedEnemiesList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
                        break;
                    case "Boss":
                        CreateBossPrompt.CreatedBoss = new Boss();
                        Map.StoredBosses.RemoveAt(CreatedBossesList.SelectedIndex);
                        CreatedBossesList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
                        break;
                    case "Buff":
                        CreateBuffPrompt.CreatedBuff = new Buff();
                        Map.StoredBuffs.RemoveAt(CreatedBuffsList.SelectedIndex);
                        CreatedBuffsList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
                        break;
                    case "Attack":
                        CreateAttackPrompt.CreatedAttack = new Attack();
                        Map.StoredAttacks.RemoveAt(CreatedAttacksList.SelectedIndex);
                        CreatedAttacksList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
                        break;
                    case "Player":
                        CreatePlayerPrompt.CreatedPlayer = new Player();
                        Map.StoredPlayers.RemoveAt(CreatedPlayersList.SelectedIndex);
                        CreatedPlayersList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
                        break;
                    case "Hazard":
                        CreateHazardPrompt.CreatedHazard = new Hazard();
                        Map.StoredHazards.RemoveAt(CreatedHazardsList.SelectedIndex);
                        CreatedHazardsList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
                        break;
                    case "Block":
                        CreateBlockPrompt.CreatedBlock = new Environment.Block();
                        Map.StoredBlocks.RemoveAt(CreatedBlocksList.SelectedIndex);
                        CreatedBlocksList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
                        break;
                    case "Item":
                        CreateItemPrompt.CreatedItem = new Item();
                        Map.StoredItems.RemoveAt(CreatedItemsList.SelectedIndex);
                        CreatedItemsList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
                        break;
                    case "Equiptment":
                        CreateEquiptmentPrompt.CreatedEquiptment = new Equiptment();
                        Map.StoredEquiptments.RemoveAt(CreatedEquiptmentsList.SelectedIndex);
                        CreatedEquiptmentsList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
                        break;
                    case "Consumable":
                        CreateConsumablePrompt.CreatedConsumable = new Consumable();
                        Map.StoredConsumables.RemoveAt(CreatedConsumablesList.SelectedIndex);
                        CreatedConsumablesList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
                        break;
                    case "Level":
                        CreateLevelPrompt.CreatedLevel = new Level();
                        Map.Levels.RemoveAt(CreatedLevelsList.SelectedIndex);
                        CreatedLevelsList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
                        break;
                    default:
                        throw new ArgumentException("Tag missing or invalid");
                }

                ClearSelection(sender, null);
            }
            catch (IndexOutOfRangeException)
            {

            }
        }

        private void CreatedEnemiesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CreateEnemyPrompt.CreatedEnemy = Map.StoredEnemies[CreatedEnemiesList.SelectedIndex];
                CreateEnemyBtn.Content = "Edit Enemy";
            }
            catch (Exception)
            {

            }
        }

        private void CreatedBossesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CreateBossPrompt.CreatedBoss = Map.StoredBosses[CreatedBossesList.SelectedIndex];
                CreateBossBtn.Content = "Edit Boss";
            }
            catch (Exception)
            {

            }
        }

        private void CreatedBuffsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CreateBuffPrompt.CreatedBuff = Map.StoredBuffs[CreatedBuffsList.SelectedIndex];
                CreateBuffBtn.Content = "Edit Buff";
            }
            catch (Exception)
            {

            }
        }

        private void CreatedAttacksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CreateAttackPrompt.CreatedAttack = Map.StoredAttacks[CreatedAttacksList.SelectedIndex];
                CreateAttackBtn.Content = "Edit Attack";
            }
            catch (Exception)
            {

            }
        }

        private void CreatedPlayersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CreatePlayerPrompt.CreatedPlayer = Map.StoredPlayers[CreatedPlayersList.SelectedIndex];
                CreatePlayerBtn.Content = "Edit Player";
            }
            catch (Exception)
            {

            }
        }

        private void CreatedHazardsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CreateHazardPrompt.CreatedHazard = Map.StoredHazards[CreatedHazardsList.SelectedIndex];
                CreateHazardBtn.Content = "Edit Hazard";
            }
            catch (Exception)
            {

            }
        }

        private void CreatedBlocksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CreateBlockPrompt.CreatedBlock = Map.StoredBlocks[CreatedBlocksList.SelectedIndex];
                CreateBlockBtn.Content = "Edit Block";
            }
            catch (Exception)
            {

            }
        }

        private void CreatedItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CreateItemPrompt.CreatedItem = Map.StoredItems[CreatedItemsList.SelectedIndex];
                CreateItemBtn.Content = "Edit Item";
            }
            catch (Exception)
            {

            }
        }

        private void CreatedEquiptmentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CreateEquiptmentPrompt.CreatedEquiptment = Map.StoredEquiptments[CreatedEquiptmentsList.SelectedIndex];
                CreateEquiptmentBtn.Content = "Edit Equiptment";
            }
            catch (Exception)
            {

            }
        }

        private void CreatedConsumablesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CreateConsumablePrompt.CreatedConsumable = Map.StoredConsumables[CreatedConsumablesList.SelectedIndex];
                CreateConsumableBtn.Content = "Edit Consumable";
            }
            catch (Exception)
            {

            }
        }

        private void CreatedLevelsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CreateLevelPrompt.CreatedLevel = Map.Levels[CreatedLevelsList.SelectedIndex];
                CreatedLevelBtn.Content = "Edit Level";
            }
            catch (Exception)
            {

            }
        }

        private void SelectIconBtn_Click(object sender, RoutedEventArgs e)
        {
            Map.IconLocation = FileHandler.SaveBitmap(Map, "icon");
        }

        private void AddIntroMessage_Click(object sender, RoutedEventArgs e)
        {
            MessageSelect messageSelect = new MessageSelect("Introduction Message", Map.IntroMessage);
            messageSelect.ShowDialog();
            Map.IntroMessage = messageSelect.GetMessage();
        }


    }
}

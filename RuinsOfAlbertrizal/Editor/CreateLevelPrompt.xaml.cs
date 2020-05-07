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
using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Editor.AdderPrompts;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.XMLInterpreter;

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreateLevelPrompt.xaml
    /// </summary>
    public partial class CreateLevelPrompt : EditorInterface
    {
        public static Level CreatedLevel { get; set; }

        public CreateLevelPrompt()
        {
            InitializeComponent();
            UpdateComponent();
            DataContext = CreatedLevel;
        }

        protected override void UpdateComponent()
        {
            if (CreatedLevel == null)
                CreatedLevel = new Level();
        }

        protected override void ClearVariable()
        {
            CreatedLevel = new Level();
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(LevelName);
        }

        private void SelectIconBtn_Click(object sender, RoutedEventArgs e)
        {
            CreatedLevel.IconLocation = FileHandler.SaveBitmap(CreatedLevel, "icon");
        }

        private void SelectWorldImgBtn_Click(object sender, RoutedEventArgs e)
        {
            CreatedLevel.WorldImgLocation = FileHandler.SaveBitmap(CreatedLevel, "worldImg");
        }

        //private void ClearSelection(object sender, RoutedEventArgs e)
        //{
        //    Button button = (Button)sender;
        //    StackPanel stackPanel = (StackPanel)VisualTreeHelper.GetParent(button);

        //    switch (stackPanel.Tag)
        //    {
        //        case "Enemy":
        //            CreatedEnemiesList.SelectedIndex = -1;
        //            CreateEnemyBtn.Content = "Create Enemy";
        //            break;
        //        case "Boss":
        //            CreatedBossesList.SelectedIndex = -1;
        //            CreateBossBtn.Content = "Create Boss";
        //            break;
        //        default:
        //            throw new ArgumentException("Tag missing or invalid");
        //    }
        //}

        //private void DeleteSelection(object sender, RoutedEventArgs e)
        //{
        //    Button button = (Button)sender;
        //    StackPanel stackPanel = (StackPanel)VisualTreeHelper.GetParent(button);

        //    try
        //    {
        //        switch (stackPanel.Tag)
        //        {
        //            case "Enemy":
        //                CreateEnemyPrompt.CreatedEnemy = new Enemy();
        //                CreatedLevel.StoredEnemies.RemoveAt(CreatedEnemiesList.SelectedIndex);
        //                CreatedEnemiesList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        //                break;
        //            case "Boss":
        //                CreateBossPrompt.CreatedBoss = new Boss();
        //                CreatedLevel.Bosses.RemoveAt(CreatedBossesList.SelectedIndex);
        //                CreatedBossesList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        //                break;
        //            case "Buff":
        //                CreateBuffPrompt.CreatedBuff = new Buff();
        //                CreatedLevel.StoredBuffs.RemoveAt(CreatedBuffsList.SelectedIndex);
        //                CreatedBuffsList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        //                break;
        //            case "Attack":
        //                CreateAttackPrompt.CreatedAttack = new Attack();
        //                CreatedLevel.StoredAttacks.RemoveAt(CreatedAttacksList.SelectedIndex);
        //                CreatedAttacksList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        //                break;
        //            case "Player":
        //                CreatePlayerPrompt.CreatedPlayer = new Player();
        //                CreatedLevel.StoredPlayers.RemoveAt(CreatedPlayersList.SelectedIndex);
        //                CreatedPlayersList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        //                break;
        //            case "Hazard":
        //                CreateHazardPrompt.CreatedHazard = new Hazard();
        //                CreatedLevel.StoredHazards.RemoveAt(CreatedHazardsList.SelectedIndex);
        //                CreatedHazardsList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        //                break;
        //            case "Block":
        //                CreateBlockPrompt.CreatedBlock = new Environment.Block();
        //                CreatedLevel.StoredBlocks.RemoveAt(CreatedBlocksList.SelectedIndex);
        //                CreatedBlocksList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        //                break;
        //            case "Item":
        //                CreateItemPrompt.CreatedItem = new Item();
        //                CreatedLevel.StoredItems.RemoveAt(CreatedItemsList.SelectedIndex);
        //                CreatedItemsList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        //                break;
        //            case "Equiptment":
        //                CreateEquiptmentPrompt.CreatedEquiptment = new Equiptment();
        //                CreatedLevel.StoredEquiptments.RemoveAt(CreatedEquiptmentsList.SelectedIndex);
        //                CreatedEquiptmentsList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        //                break;
        //            case "Consumable":
        //                CreateConsumablePrompt.CreatedConsumable = new Consumable();
        //                CreatedLevel.StoredConsumables.RemoveAt(CreatedConsumablesList.SelectedIndex);
        //                CreatedConsumablesList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        //                break;
        //            default:
        //                throw new ArgumentException("Tag missing or invalid");
        //        }

        //        ClearSelection(sender, null);
        //    }
        //    catch (IndexOutOfRangeException)
        //    {

        //    }
        //}

        //private void CreatedEnemiesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        CreateEnemyPrompt.CreatedEnemy = CreatedLevel.StoredEnemies[CreatedEnemiesList.SelectedIndex];
        //        CreateEnemyBtn.Content = "Edit Enemy";
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        //private void CreatedBossesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        CreateBossPrompt.CreatedBoss = CreatedLevel.Bosses[CreatedBossesList.SelectedIndex];
        //        CreateBossBtn.Content = "Edit Boss";
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        private void AddBossBtn_Click(object sender, RoutedEventArgs e)
        {
            BossAdderPrompt bossAdderPrompt = new BossAdderPrompt(CreatedLevel.Bosses);
            bossAdderPrompt.ShowDialog();
            CreatedLevel.Bosses = bossAdderPrompt.TargetBosses;
        }

        private void AddEnemyBtn_Click(object sender, RoutedEventArgs e)
        {
            SimpleAdderPrompt simpleAdderPrompt = new SimpleAdderPrompt(CreatedLevel.StoredEnemies.Cast<ObjectOfAlbertrizal>().ToList(), CreateMapPrompt.Map.StoredEnemies.Cast<ObjectOfAlbertrizal>().ToList(), "Add/Remove Enemies");
            simpleAdderPrompt.ShowDialog();
            CreatedLevel.Bosses = simpleAdderPrompt.StoredObjects.Cast<Boss>().ToList();
        }

        private void AddIntroMessage_Click(object sender, RoutedEventArgs e)
        {
            MessageSelect messageSelect = new MessageSelect("Introduction Message", CreatedLevel.IntroMessage);
            messageSelect.ShowDialog();
            CreatedLevel.IntroMessage = messageSelect.GetMessage();
        }
    }
}

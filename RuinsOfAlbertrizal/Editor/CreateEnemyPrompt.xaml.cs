using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Windows;

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreateEnemyPrompt.xaml
    /// </summary>
    public partial class CreateEnemyPrompt : EditorInterface
    {
        public static Enemy CreatedEnemy { get; set; }
        
        public CreateEnemyPrompt() : base()
        {
            InitializeComponent();
            DataContext = CreatedEnemy;
        }

        public CreateEnemyPrompt(Map map) : base(map)
        {
            InitializeComponent();
            DataContext = CreatedEnemy;
        }

        protected override void UpdateComponent()
        {
            if (CreatedEnemy == null)
                CreatedEnemy = new Enemy();
        }

        public override void ClearVariable()
        {
            CreatedEnemy = new Enemy();
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(EnemyName);
            RequiredControls.Add(BaseHP);
            RequiredControls.Add(BaseMana);
            RequiredControls.Add(BaseSpd);
            RequiredControls.Add(BaseDef);
            RequiredControls.Add(BaseDmg);
            RequiredControls.Add(BaseInt);
            RequiredControls.Add(SpawnChance);
        }

        private void SelectIconBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreatedEnemy.IconLocation = FileHandler.SaveBitmap(CreatedEnemy, "icon");
            }
            catch (ArgumentException)
            {

            }
        }

        private void SelectWorldImgBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreatedEnemy.WorldImgLocation = FileHandler.SaveBitmap(CreatedEnemy, "worldImg");
            }
            catch (ArgumentException)
            {

            }
        }
    }
}

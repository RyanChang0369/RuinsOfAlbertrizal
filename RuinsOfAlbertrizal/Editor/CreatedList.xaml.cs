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
    /// Interaction logic for CreatedList.xaml
    /// </summary>
    public partial class CreatedList : UserControl
    {
        private string objectBeingCreated;
        public string ObjectBeingCreated
        {
            get => objectBeingCreated;
            set
            {
                HeaderLbl.Content = "Create " + value;
                objectBeingCreated = value;
            }
        }

        public Button CreateBtn { get; set; }

        public CreatedList()
        {
            InitializeComponent();
        }

        private void CreatedObjectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClearSelection(object sender, RoutedEventArgs e)
        {
            CreatedObjectList.SelectedIndex = -1;
            CreateBtn.Content = "Create Enemy";
        }

        private void DeleteSelection(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateEnemyPrompt.CreatedEnemy = new Enemy();
                CreateMapPrompt.Map.StoredEnemies.RemoveAt(CreatedEnemiesList.SelectedIndex);
                CreatedEnemiesList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
                ClearSelection(sender, null);
            }
            catch (Exception)
            {

            }
        }
    }
}

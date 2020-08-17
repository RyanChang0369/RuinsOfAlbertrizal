using RuinsOfAlbertrizal.Editor;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
            Testing.InitTest();
        }

        private void NewCustomMap(object sender, RoutedEventArgs e)
        {
            try
            {
                FileHandler.CreateCustomCampaign();
            }
            catch (Exception ex)
            {
                return;
            }

            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }

        private void EditCustomMap(object sender, RoutedEventArgs e)
        {
            try
            {
                FileHandler.LoadCustomCampaign(true);
            }
            catch (ArgumentNullException)
            {
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Project File Cannot Be Read! Advanced Error Message:\r\n{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ContinueCustomMap(object sender, RoutedEventArgs e)
        {
            try
            {
                FileHandler.LoadCustomCampaign(false);
            }
            catch (ArgumentNullException)
            {
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Project File Cannot Be Read! Advanced Error Message:\r\n{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            NavIntroInterface(GameBase.CurrentGame);
        }

        private void NewCampaign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileHandler.NewCampaign();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"Important game files could not be found! Have the Campaign folder been altered? Have the exe file been moved from bin directory?\r\n\r\n{ex.Message}");
                throw;
            }

            NavIntroInterface(GameBase.CurrentGame);
        }

        private void ContinueCampaign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileHandler.LoadCampaign();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"Important game files could not be found! Have the Campaign folder been altered? Have the exe file been moved from bin directory?\r\n\r\n{ex.Message}");
                throw;
            }

            NavIntroInterface(GameBase.CurrentGame);
        }

        private void NavIntroInterface(Map currentMap)
        {
            if (!currentMap.PlayerCreated && currentMap.AllowForPlayerCreation)
                NavigationService.Navigate(new Uri("PlayerCreatePage.xaml", UriKind.RelativeOrAbsolute));
            else if ((currentMap.IntroMessage == null || currentMap.IntroMessage.IsEmpty()) &&
                (currentMap.CurrentLevel.IntroMessage == null || currentMap.CurrentLevel.IntroMessage.IsEmpty()))
                NavigationService.Navigate(new Uri("ExploreInterface.xaml", UriKind.RelativeOrAbsolute));
            else if (currentMap.IntroMessage == null || currentMap.IntroMessage.IsEmpty())
                NavigationService.Navigate(new Uri("LevelIntroInterface.xaml"), UriKind.RelativeOrAbsolute);
            else if (currentMap.SeenIntroduction && currentMap.CurrentLevel.SeenIntroduction)
                NavigationService.Navigate(new Uri("ExploreInterface.xaml", UriKind.RelativeOrAbsolute));
            else if (currentMap.SeenIntroduction)
                NavigationService.Navigate(new Uri("LevelIntroInterface.xaml"), UriKind.RelativeOrAbsolute);
            else
                NavigationService.Navigate(new Uri("IntroInterface.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ResetCustomMap(object sender, RoutedEventArgs e)
        {
            if (GameBase.CurrentMapLocation == null || GameBase.StaticMapLocation == null)
            {
                try
                {
                    FileDialog dialog = new FileDialog(FileDialog.DialogOptions.Open, "XAML File | map.xml");

                    GameBase.CurrentMapLocation = dialog.GetPath();
                    GameBase.StaticMapLocation = System.IO.Path.GetDirectoryName(dialog.GetPath()) + "\\map-static.xml";

                    GameBase.StaticGame = FileHandler.LoadMap(GameBase.StaticMapLocation);
                }
                catch (ArgumentNullException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Project File Cannot Be Read! Advanced Error Message:\r\n{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            GameValidator.Validate(GameBase.StaticGame);

            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset the map? This action cannot be undone.", "Confirm Reset", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                GameBase.CurrentGame = GameBase.StaticGame;
                FileHandler.SaveStaticMap();
                FileHandler.SaveCurrentMap();

                MessageBox.Show("Map has been reset.");
            }
        }

        private void Reference(object sender, RoutedEventArgs e)
        {
            //HelpSection helpSection = new HelpSection();
            //helpSection.Show();
            string tempDirectory = $"{Path.GetTempPath()}RuinsOfAlbertrizal\\";
            Directory.CreateDirectory(tempDirectory);
            string fileName = $"{tempDirectory}Help.pdf";

            File.WriteAllBytes(fileName, Properties.Resources.Help);

            try
            {
                Process.Start(fileName);
            }
            catch (Exception)
            {

            }
        }
    }
}

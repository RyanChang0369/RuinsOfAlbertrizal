using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for ExploreInterface.xaml
    /// </summary>
    public partial class ExploreInterface : BasePage
    {
        private Timer imgZoomTimer = new Timer(10);

        public ExploreInterface()
        {
            InitializeComponent();
            DataContext = GameBase.CurrentGame.CurrentLevel;
            Title = $"Exploring {GameBase.CurrentGame.CurrentLevel.Name}";
            imgZoomTimer.Elapsed += ZoomImg;
        }

        private void ExploreBtn_Click(object sender, RoutedEventArgs e)
        {
            //DoubleAnimation animation = new DoubleAnimation
            //{
            //    Duration = new Duration(TimeSpan.FromSeconds(3)),
            //    From = 1.1,
            //    To = 4.0
            //};

            //imgZoomTimer.Start();
            //ExploreBtn.IsEnabled = false;

            //mainImage.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            //mainImage.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        private void ZoomImg(object sender, ElapsedEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    ScaleTransform scaleTransform = (ScaleTransform)mainImage.RenderTransform;

                    scaleTransform.ScaleX += 0.05;
                    scaleTransform.ScaleY += 0.05;

                    if (scaleTransform.ScaleX >= 4.0)
                    {
                        imgZoomTimer.Stop();
                        ExploreBtn.IsEnabled = true;
                    }
                });
            }
            catch (Exception)
            {

            }
        }
    }
}

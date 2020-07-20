using RuinsOfAlbertrizal.Characters;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Point = System.Drawing.Point;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for CharacterImage.xaml
    /// </summary>
    public partial class CharacterImage : UserControl
    {
        public enum TargetMode
        {
            AwaitingSelection,
            AwaitingConfirmation,
            Confirmed
        }

        private TargetMode Mode { get; set; }

        private Point OldLocation { get; set; }

        public static readonly DependencyProperty AssociatedCharacterProperty =
            DependencyProperty.Register
            ("AssociatedCharacter", typeof(Character), typeof(CharacterImage),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnAssociatedCharacterChanged)));

        public Character AssociatedCharacter
        {
            get => (Character)GetValue(AssociatedCharacterProperty);
            set { SetValue(AssociatedCharacterProperty, value); }
        }

        public static readonly DependencyProperty BaseImageSourceProperty =
            DependencyProperty.Register
            (
                "BaseImageSource",
                typeof(BitmapSource),
                typeof(CharacterImage),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnBaseImageSourceChanged))
            );

        public BitmapSource BaseImageSource
        {
            get { return (BitmapSource)GetValue(BaseImageSourceProperty); }
            set { SetValue(BaseImageSourceProperty, value); }
        }

        public static readonly RoutedEvent RequestDetailedStatsEvent =
            EventManager.RegisterRoutedEvent("RequestDetailedStats", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(CharacterImage));

        /// <summary>
        /// Requests that detailed stats to be shown.
        /// </summary>
        public event RoutedEventHandler RequestDetailedStats
        {
            add { AddHandler(RequestDetailedStatsEvent, value); }
            remove { RemoveHandler(RequestDetailedStatsEvent, value); }
        }

        public static readonly RoutedEvent TargetSelectEvent =
            EventManager.RegisterRoutedEvent("TargetSelect", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(CharacterImage));

        /// <summary>
        /// Requests that this CharacterImage to be selected as a target.
        /// </summary>
        public event EventHandler TargetSelect
        {
            add { AddHandler(TargetSelectEvent, value); }
            remove { RemoveHandler(TargetSelectEvent, value); }
        }

        public static readonly RoutedEvent TargetDeselectEvent =
            EventManager.RegisterRoutedEvent("TargetDeselect", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(CharacterImage));

        /// <summary>
        /// Requests that this CharacterImage to be deselected as a target.
        /// </summary>
        public event EventHandler TargetDeselect
        {
            add { AddHandler(TargetDeselectEvent, value); }
            remove { RemoveHandler(TargetDeselectEvent, value); }
        }

        public static readonly RoutedEvent TargetConfirmEvent =
            EventManager.RegisterRoutedEvent("TargetConfirm", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(CharacterImage));

        /// <summary>
        /// Requests that this target be confirmed as a target.
        /// </summary>
        public event EventHandler TargetConfirm
        {
            add { AddHandler(TargetConfirmEvent, value); }
            remove { RemoveHandler(TargetConfirmEvent, value); }
        }

        public static readonly RoutedEvent TargetDeconfirmEvent =
            EventManager.RegisterRoutedEvent("TargetDeconfirm", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(CharacterImage));

        /// <summary>
        /// Requests that this target be deconfirmed as a target.
        /// </summary>
        public event EventHandler TargetDeconfirm
        {
            add { AddHandler(TargetDeconfirmEvent, value); }
            remove { RemoveHandler(TargetDeconfirmEvent, value); }
        }

        //private static readonly RoutedEvent BaseImageMoveEvent =
        //    EventManager.RegisterRoutedEvent("BaseImageMove", RoutingStrategy.Bubble,
        //    typeof(RoutedEventHandler), typeof(CharacterImage));

        //private event EventHandler BaseImageMove
        //{
        //    add { AddHandler(BaseImageMoveEvent, value); }
        //    remove { RemoveHandler(BaseImageMoveEvent, value); }
        //}

        public CharacterImage()
        {
            InitializeComponent();
        }

        public CharacterImage(Character character)
        {
            InitializeComponent();
            BaseImageSource = character.ArmoredImageAsBitmapSource;
            AssociatedCharacter = character;
        }

        public void CharacterAttack(Point attackerPoint, Point targetPoint)
        {
            if (attackerPoint.X < targetPoint.X)
                Animate("simpleAttackRight", BaseImage);
            else
                Animate("simpleAttackLeft", BaseImage);
        }

        public void CharcterCharge()
        {
            Animate("chargeAttack", BaseImage);
        }

        public void CharacterDeath()
        {
            if (AssociatedCharacter.GetType() == typeof(Player))
            {
                Animate("deathLeft", BaseImage);
                return;
            }
            else
            {
                Animate("deathRight", BaseImage);
                return;
            }
        }

        public void CharacterRevive()
        {

        }

        /// <summary>
        /// Moves the AssociatedCharacter from oldLocation to AssociatedCharacter.BattleFieldLocation.
        /// </summary>
        /// <param name="oldLocation"></param>
        /// <returns>True when if successful.</returns>
        public async Task<bool> CharacterMove(Point oldLocation, double columnWidth, double rowHeight)
        {
            try
            {
                OldLocation = oldLocation;

                double distance = MiscMethods.DistanceFormula(AssociatedCharacter.BattleFieldLocation, oldLocation);
                int delayTime = (int)Math.Round(2000 * (distance / 5));

                DoubleAnimation animationX = new DoubleAnimation((AssociatedCharacter.BattleFieldLocation.X - oldLocation.X) * columnWidth, TimeSpan.FromMilliseconds(delayTime));
                DoubleAnimation animationY = new DoubleAnimation((AssociatedCharacter.BattleFieldLocation.Y - oldLocation.Y) * rowHeight, TimeSpan.FromMilliseconds(delayTime));

                BaseImageTranslateTransform.BeginAnimation(TranslateTransform.XProperty, animationX);
                BaseImageTranslateTransform.BeginAnimation(TranslateTransform.YProperty, animationY);

                await MiscMethods.TaskDelay(delayTime);

                this.GetBindingExpression(Grid.RowProperty).UpdateTarget();
                this.GetBindingExpression(Grid.ColumnProperty).UpdateTarget();

                BaseImageTranslateTransform = new TranslateTransform();
                TransformGroup transformGroup = (TransformGroup)BaseImage.RenderTransform;
                transformGroup.Children[2] = BaseImageTranslateTransform;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void Select()
        {
            RaiseEvent(new RoutedEventArgs(TargetSelectEvent));
            TargetImage.Visibility = Visibility.Visible;
            Animate("targetFadeIn", TargetImage);
            Mode = TargetMode.AwaitingConfirmation;
        }

        public void Deselect()
        {
            RaiseEvent(new RoutedEventArgs(TargetDeselectEvent));
            Animate("targetFadeOut", TargetImage);
            TargetImage.Visibility = Visibility.Collapsed;
            Mode = TargetMode.AwaitingSelection;
        }

        public void FadeOut()
        {
            Animate("targetFadeOut", TargetImage);
            TargetImage.Visibility = Visibility.Collapsed;
        }

        private void Confirm()
        {
            RaiseEvent(new RoutedEventArgs(TargetConfirmEvent));
            Animate("targetConfirm", TargetImage);
            Mode = TargetMode.Confirmed;
        }

        private void Deconfirm()
        {
            RaiseEvent(new RoutedEventArgs(TargetDeconfirmEvent));
            Animate("targetDeconfirm", TargetImage);
            Mode = TargetMode.AwaitingConfirmation;
        }

        private static void OnBaseImageSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CharacterImage charImg = (CharacterImage)sender;
            if (charImg != null)
            {
                charImg.BaseImage.Source = (BitmapSource)e.NewValue;
            }
        }

        private static void OnAssociatedCharacterChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CharacterImage charImg = (CharacterImage)sender;
            if (charImg != null)
            {
                Character character = (Character)e.NewValue;
                charImg.AssociatedCharacter = character;
                charImg.BaseImageSource = character.ArmoredImageAsBitmapSource;

                if (character.GetType() == typeof(Player))
                {
                    charImg.BaseImageScaleTransform.ScaleX = 1;
                }
                else
                {
                    charImg.BaseImageScaleTransform.ScaleX = -1;
                }
            }
        }

        private void TargetImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (Mode)
            {
                case TargetMode.Confirmed:
                    Deconfirm();
                    break;
                case TargetMode.AwaitingConfirmation:
                    Confirm();
                    break;
            }
        }

        private void BaseImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(RequestDetailedStatsEvent));
        }

        public void InitialAnimation()
        {
            if (AssociatedCharacter == null)
                return;
            else if (AssociatedCharacter.GetType() == typeof(Player))
            {
                Animate("playerSlideIn", BaseImage);
            }
            else
            {
                Animate("enemySlideIn", BaseImage);
            }
        }

        public void Animate(string storyboardName, FrameworkElement element)
        {
            Storyboard storyboard = (Storyboard)Resources[storyboardName];
            storyboard.Begin(element);
        }

    }
}

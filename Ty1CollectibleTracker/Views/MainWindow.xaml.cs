using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Ty1CollectibleTracker.Views;

namespace Ty1CollectibleTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _solidBack = false;
        
        public MainWindow()
        {
            InitializeComponent();
            LoadPositions();
        }

        private void LoadPositions()
        {
            PositionStorageHandler.Load();
            Canvas.SetLeft(Bilby, PositionStorageHandler.Positions.BilbyPosition[0]);
            Canvas.SetTop(Bilby, PositionStorageHandler.Positions.BilbyPosition[1]);
            Canvas.SetLeft(TE, PositionStorageHandler.Positions.TEPosition[0]);
            Canvas.SetTop(TE, PositionStorageHandler.Positions.TEPosition[1]);
            Canvas.SetLeft(Cog, PositionStorageHandler.Positions.CogPosition[0]);
            Canvas.SetTop(Cog, PositionStorageHandler.Positions.CogPosition[1]);
            Canvas.SetLeft(Talisman, PositionStorageHandler.Positions.TalismanPosition[0]);
            Canvas.SetTop(Talisman, PositionStorageHandler.Positions.TalismanPosition[1]);
            Canvas.SetLeft(Opal, PositionStorageHandler.Positions.OpalPosition[0]); 
            Canvas.SetTop(Opal, PositionStorageHandler.Positions.OpalPosition[1]);
            Canvas.SetLeft(Bilby, PositionStorageHandler.Positions.BilbyPosition[0]);
            Canvas.SetTop(Bilby, PositionStorageHandler.Positions.BilbyPosition[1]);
            Canvas.SetLeft(Rang, PositionStorageHandler.Positions.RangPosition[0]);
            Canvas.SetTop(Rang, PositionStorageHandler.Positions.RangPosition[1]);
            Canvas.SetLeft(Frame, PositionStorageHandler.Positions.FramePosition[0]);
            Canvas.SetTop(Frame, PositionStorageHandler.Positions.FramePosition[1]);
            Width = PositionStorageHandler.Positions.WindowSize[0];
            Height = PositionStorageHandler.Positions.WindowSize[1];
            Left = PositionStorageHandler.Positions.WindowPosition[0];
            Top = PositionStorageHandler.Positions.WindowPosition[1];
            WindowState = PositionStorageHandler.Positions.WindowMaximized ? WindowState.Maximized : WindowState.Normal;
        }

        private void SavePositions()
        {
            PositionStorageHandler.Positions.BilbyPosition[0] = Canvas.GetLeft(Bilby);
            PositionStorageHandler.Positions.BilbyPosition[1] = Canvas.GetTop(Bilby);
            PositionStorageHandler.Positions.TEPosition[0] = Canvas.GetLeft(TE);
            PositionStorageHandler.Positions.TEPosition[1] = Canvas.GetTop(TE);
            PositionStorageHandler.Positions.CogPosition[0] = Canvas.GetLeft(Cog);
            PositionStorageHandler.Positions.CogPosition[1] = Canvas.GetTop(Cog);
            PositionStorageHandler.Positions.TalismanPosition[0] = Canvas.GetLeft(Talisman);
            PositionStorageHandler.Positions.TalismanPosition[1] = Canvas.GetTop(Talisman);
            PositionStorageHandler.Positions.OpalPosition[0] = Canvas.GetLeft(Opal);
            PositionStorageHandler.Positions.OpalPosition[1] = Canvas.GetTop(Opal);
            PositionStorageHandler.Positions.RangPosition[0] = Canvas.GetLeft(Rang);
            PositionStorageHandler.Positions.RangPosition[1] = Canvas.GetTop(Rang);
            PositionStorageHandler.Positions.FramePosition[0] = Canvas.GetLeft(Frame);
            PositionStorageHandler.Positions.FramePosition[1] = Canvas.GetTop(Frame);
            PositionStorageHandler.Positions.WindowSize[0] = Width;
            PositionStorageHandler.Positions.WindowSize[1] = Height;
            PositionStorageHandler.Positions.WindowPosition[0] = Left;
            PositionStorageHandler.Positions.WindowPosition[1] = Top;
            PositionStorageHandler.Positions.WindowMaximized = WindowState == WindowState.Maximized;
            PositionStorageHandler.Save();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && (e.Source == GrabBar || e.Source == Canvas))
            {
                WindowState = WindowState.Normal;
                DragMove();
            }
        }

        private void Canvas_OnDrag(object sender, DragEventArgs e)
        {
            var dropPos = e.GetPosition(Canvas);
            Canvas.SetLeft(e.Source as UIElement, dropPos.X - 40);
            Canvas.SetTop(e.Source as UIElement, dropPos.Y - 30);
        }

        private object? collectible;
        
        private void Collectible_MouseMove(object? sender, MouseEventArgs e)
        {
            if (collectible != sender) return;
            DragDrop.DoDragDrop(collectible as UIElement, collectible as UIElement, DragDropEffects.Move);
            if (e.LeftButton == MouseButtonState.Pressed || collectible == null) return;
            Canvas.SetZIndex(collectible as UIElement, 2);
            collectible = null;
        }
        
        private void Collectible_MouseDown(object? sender, MouseButtonEventArgs e)
        {
            if (sender is UIElement && collectible == null)
            {
                collectible = sender;
                Canvas.SetZIndex(sender as UIElement, 200);
            }
        }
        
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).Running = false;
            App.Current.Shutdown();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settingsMenu = new SettingsView();
            settingsMenu.ShowDialog();
        }

        private void SetSolidBack_Click(object sender, RoutedEventArgs e)
        {
            _solidBack = !_solidBack;
            Canvas.Background = _solidBack ? 
                new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)) : 
                new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
        {
            (DataContext as MainWindowViewModel).Running = false;
            SavePositions();
        }

        private void GrabBar_OnMouseEnter(object sender, MouseEventArgs e)
        {
            GrabBar.Fill = Brushes.White;
        }

        private void GrabBar_OnMouseLeave(object sender, MouseEventArgs e)
        {
            GrabBar.Fill = Brushes.Transparent;
        }
    }
}
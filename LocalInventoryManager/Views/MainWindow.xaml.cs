using System;
using System.Windows;
using System.Windows.Forms;

namespace LocalInventoryManager.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(
                new Action(CenterWindowOnScreen),
                System.Windows.Threading.DispatcherPriority.ApplicationIdle
            );
        }

        private void CenterWindowOnScreen()
        {
            var area = Screen.PrimaryScreen.WorkingArea;

            Left = area.Left + (area.Width - ActualWidth) / 2;
            Top = area.Top + (area.Height - ActualHeight) / 2;
        }
    }
}
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Krema_Roster_Maker.Model;
using System.Configuration;


namespace Krema_Roster_Maker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new StaffListPage();

        }
       
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private bool isMaximized= false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2) { 
                if (isMaximized)
                {
                    this.WindowState =WindowState.Normal;
                    this.Height = 720;
                    this.Width = 1080;
                    isMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    isMaximized= true;
                }
            }
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string pageName)
            {
                switch (pageName)
                {
                    case "StaffListPage":
                        MainContent.Content = new StaffListPage();
                        break;
                    case "ShiftsPage":
                        MainContent.Content = new ShiftsPage();
                        break;
                    case "RosterMakerPage":
                        MainContent.Content = new RosterMakerPage();
                        break;
                    case "SettingsPage":
                        MainContent.Content = new SettingsPage();
                        break;
                    case "ExitPage":
                        Application.Current.Shutdown();
                        break;
                }
            }
        }
       

    }
}
using Krema_Roster_Maker.Context;
using Krema_Roster_Maker.Helpers;
using Krema_Roster_Maker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Krema_Roster_Maker
{
    /// <summary>
    /// Interaction logic for StaffListPage.xaml
    /// </summary>
    public partial class StaffListPage : UserControl
    {
        public StaffListPage()
        {
            InitializeComponent();
            var contextRepository = new JsonContextRepository("appContext.json");
            membersDataGrid.ItemsSource = contextRepository.Context.Staffs;
        }

        private void AddStaffButton_Click(object sender, RoutedEventArgs e)
        {

            var modal = new EditStaffWindow();
            modal.Owner = MainWindow.GetWindow(this);
            var x = modal.ShowDialog();
            if (x == true)
            {
                var contextRepository = new JsonContextRepository("appContext.json");
                membersDataGrid.ItemsSource = contextRepository.Context.Staffs;
            }

        }

        private void EditStaffButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Staff staff)
            {

                var modal = new EditStaffWindow(staff);
                modal.Owner = MainWindow.GetWindow(this);
                var x = modal.ShowDialog();
                if (x == true)
                {
                    var contextRepository = new JsonContextRepository("appContext.json");
                    membersDataGrid.ItemsSource = contextRepository.Context.Staffs;
                }
            }
        }
    }
}

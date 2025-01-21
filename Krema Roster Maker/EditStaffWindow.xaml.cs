using Krema_Roster_Maker.Model;
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
using System.Windows.Shapes;

namespace Krema_Roster_Maker
{
    /// <summary>
    /// Interaction logic for EditStaffWindow.xaml
    /// </summary>
    public partial class EditStaffWindow : Window
    {

        public Staff SelectedStaff { get; set; }

        public EditStaffWindow(Staff staff)
        {
            //SelectedStaff = staff;
            DataContext = staff;
            InitializeComponent();
            PositionComboBox.ItemsSource = Enum.GetValues(typeof(Positions)).Cast<Positions>();
            WorkTypeComboBox.ItemsSource = Enum.GetValues(typeof(WorkType)).Cast<WorkType>();

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {


            // Notify success and close modal
            MessageBox.Show("Staff details updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Visibility = Visibility.Collapsed; // Hide modal
        }

   
    }
}

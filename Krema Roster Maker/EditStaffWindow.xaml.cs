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
            SelectedStaff = staff;
            DataContext = this;
            InitializeComponent();

            // Bind availability to the ItemsControl
            AvailabilityItemsControl.ItemsSource = staff.Availability.DayAvailability.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Update the staff properties
            SelectedStaff.Name = NameTextBox.Text;
            SelectedStaff.Position = (Positions)Enum.Parse(typeof(Positions), ((ComboBoxItem)PositionComboBox.SelectedItem).Content.ToString());
            SelectedStaff.WorkType = (WorkType)Enum.Parse(typeof(WorkType), ((ComboBoxItem)WorkTypeComboBox.SelectedItem).Content.ToString());

            // Update availability
            foreach (var item in AvailabilityItemsControl.Items)
            {
                var pair = (KeyValuePair<Days, (TimeOnly Start, TimeOnly End)>)item;
                TimeOnly.TryParse(((TextBox)((StackPanel)item).Children[1]).Text, out TimeOnly start);
                TimeOnly.TryParse(((TextBox)((StackPanel)item).Children[3]).Text, out TimeOnly end);
                SelectedStaff.Availability.DayAvailability[pair.Key] = (start, end);
            }

            // Notify success and close modal
            MessageBox.Show("Staff details updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Visibility = Visibility.Collapsed; // Hide modal
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed; // Hide modal without saving
        }
    }
}

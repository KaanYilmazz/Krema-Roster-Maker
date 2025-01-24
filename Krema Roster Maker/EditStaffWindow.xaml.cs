using Krema_Roster_Maker.Context;
using Krema_Roster_Maker.Model;
using System.Windows;

namespace Krema_Roster_Maker
{
    /// <summary>
    /// Interaction logic for EditStaffWindow.xaml
    /// </summary>
    public partial class EditStaffWindow : Window
    {

        public Staff? SelectedStaff { get; set; }

        public EditStaffWindow()
        {
            //SelectedStaff = staff;
            SelectedStaff = new Staff();
            DataContext = SelectedStaff;
            InitializeComponent();
            PositionComboBox.ItemsSource = Enum.GetValues(typeof(Positions)).Cast<Positions>();
            WorkTypeComboBox.ItemsSource = Enum.GetValues(typeof(WorkType)).Cast<WorkType>();

        }
        public EditStaffWindow(Staff staff)
        {
            SelectedStaff = staff;
            DataContext = SelectedStaff;
            InitializeComponent();
            PositionComboBox.ItemsSource = Enum.GetValues(typeof(Positions)).Cast<Positions>();
            WorkTypeComboBox.ItemsSource = Enum.GetValues(typeof(WorkType)).Cast<WorkType>();

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedStaff != null)
            {
                var contextRepository = new JsonContextRepository("appContext.json");
                var editStaff = contextRepository.Context.Staffs.FirstOrDefault(x=>x.Id==SelectedStaff.Id);
                if (editStaff != null)
                {
                    editStaff.Name = SelectedStaff.Name;
                    editStaff.Position = SelectedStaff.Position;
                   editStaff.WorkType = SelectedStaff.WorkType;
                    editStaff.Availability = SelectedStaff.Availability;
                    contextRepository.Save();
                }
                else
                {
                    contextRepository.Context.Staffs.Add(SelectedStaff);
                    contextRepository.Save();
                }
                // Notify success and close modal
                MessageBox.Show(SelectedStaff.Name + " added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No staff added!.", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

            Visibility = Visibility.Collapsed; // Hide modal
            this.DialogResult = true;

        }
    }
}





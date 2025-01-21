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
            ObservableCollection<Staff> members = new ObservableCollection<Staff>();
            var rominaAvailability = new Availability();
            rominaAvailability.SetAvailability(Days.Monday,new TimeOnly(12, 0), new TimeOnly(23, 59));
            rominaAvailability.SetAvailability(Days.Tuesday, false, true);
            rominaAvailability.SetAvailability(Days.Wednesday, true, false);
            rominaAvailability.SetAvailability(Days.Thursday, true, false);
            rominaAvailability.SetAvailability(Days.Friday, true, false);
            rominaAvailability.SetAvailability(Days.Saturday, true, false);
            rominaAvailability.SetAvailability(Days.Sunday, true, false);
            members.Add(new Staff("Romina", rominaAvailability, Positions.Manager, WorkType.FulTime));
            members.Add(new Staff("Deniz", rominaAvailability, Positions.AssistantManager, WorkType.FulTime));
            members.Add(new Staff("Sebo", rominaAvailability, Positions.HeadBarista, WorkType.FulTime));


            membersDataGrid.ItemsSource = members;
        }

        private void MakeRoster(List<Shift> shifts, List<Staff> staffList)
        {


            // Dictionary to store roster assignments
            Dictionary<Shift, Staff> roster = new Dictionary<Shift, Staff>();

            // Sort shifts by start time for logical assignment order
            shifts.Sort((s1, s2) => s1.StartTime.CompareTo(s2.StartTime));

            // Split staff into full-time and part-time groups
            var fullTimeStaff = staffList.Where(s => s.WorkType == WorkType.FulTime).ToList();
            var partTimeStaff = staffList.Where(s => s.WorkType == WorkType.PartTime).ToList();

            // Assign shifts
            foreach (var shift in shifts)
            {
                // Find all staff available for this shift
                var availableStaff = staffList
                    .Where(staff => staff.IsAvailableForShift(shift))
                    .OrderBy(staff => staff.Shifts.Count) // Balance workload
                    .ToList();

                // Assign to the first eligible staff member
                Staff selectedStaff = availableStaff.FirstOrDefault();

                if (selectedStaff != null)
                {
                    selectedStaff.Shifts.Add(shift);
                    roster[shift] = selectedStaff;
                }
                else
                {
                    MessageBox.Show($"No available staff for shift: {shift.StartTime}");
                }
            }

            // Output the roster
            foreach (var entry in roster)
            {
                MessageBox.Show($"Shift: {entry.Key.StartTime} -> Staff: {entry.Value.Name}");
            }
        }
        private void EditStaffButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Staff staff)
            {
                
                var modal = new EditStaffWindow(staff);
                modal.Owner = MainWindow.GetWindow(this);
                modal.ShowDialog();
            }
        }
    }
}

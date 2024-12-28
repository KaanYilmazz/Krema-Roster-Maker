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

            var converter = new BrushConverter();
            ObservableCollection<Staff> members = new ObservableCollection<Staff>();

            members.Add(new Staff("Romina", null, Positions.Manager, WorkType.FulTime));
            members.Add(new Staff("Deniz", null, Positions.AssistantManager, WorkType.FulTime));
            members.Add(new Staff("Sebo", null, Positions.HeadBarista, WorkType.FulTime));
           
          
            membersDataGrid.ItemsSource = members;
            MockData();
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
        private void MockData()
        {
            //mock data
            List<Shift> shifts = new List<Shift>();
            List<Staff> staffList = new List<Staff>();
            shifts.Add(new Shift(Days.Monday, new TimeOnly(6,30) , new TimeOnly(14,00)));
            shifts.Add(new Shift(Days.Monday, new TimeOnly(14,00) , new TimeOnly(18,00)));
            //shifts.Add(new Shift(Days.Monday, new TimeOnly(8,00) , new TimeOnly(16,00)));
            //shifts.Add(new Shift(Days.Monday, new TimeOnly(11,00) , new TimeOnly(17,00)));
            //shifts.Add(new Shift(Days.Monday, new TimeOnly(12,00) , new TimeOnly(17,00)));


            var rominaAvailability = new Availability();
            rominaAvailability.SetAvailability(Days.Monday, new TimeOnly(6, 0), new TimeOnly(18, 0));
            var panosAvailability = new Availability();
            panosAvailability.SetAvailability(Days.Monday, new TimeOnly(6, 0), new TimeOnly(14, 0));
            var pinarAvailability = new Availability();
            pinarAvailability.SetAvailability(Days.Monday, new TimeOnly(9, 0), new TimeOnly(23, 0));
            var kaanAvailability = new Availability();
            kaanAvailability.SetAvailability(Days.Monday, new TimeOnly(12, 0), new TimeOnly(23, 0));

            var romina = new Staff("Romina", rominaAvailability, Positions.Manager , WorkType.FulTime);
            var panos = new Staff("Panos", panosAvailability, Positions.HeadBarista , WorkType.FulTime);
          //  var pinar = new Staff("Pinar", pinarAvailability, Positions.RegularStaff , WorkType.FulTime);
          //  var kaan = new Staff("Kaan", kaanAvailability, Positions.RegularStaff , WorkType.PartTime);
            staffList.Add(romina);
            staffList.Add(panos);
           // staffList.Add(pinar);
          //  staffList.Add(kaan);

            MakeRoster(shifts, staffList);
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

    }
}
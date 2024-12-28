using Krema_Roster_Maker.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Krema_Roster_Maker
{
    public partial class ShiftsPage : UserControl
    {
        // Data structure to store shifts for each day
        private Dictionary<string, List<Shift>> ShiftsData = new Dictionary<string, List<Shift>>();

        public ShiftsPage()
        {
            InitializeComponent();

            // Initialize data for each day
            foreach (string day in Enum.GetNames(typeof(DayOfWeek)))
            {
                ShiftsData[day] = new List<Shift>();
            }

            // Select the first day by default
            DaysListBox.SelectedIndex = 0;
        }

        private void DaysListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DaysListBox.SelectedItem is ListBoxItem selectedItem)
            {
                string selectedDay = selectedItem.Content.ToString();
                UpdateShiftsListView(selectedDay);
            }
        }

        private void UpdateShiftsListView(string day)
        {
            if (ShiftsData.ContainsKey(day))
            {
                ShiftsListView.ItemsSource = null;
                ShiftsListView.ItemsSource = ShiftsData[day];
            }
        }

        private void AddShiftButton_Click(object sender, RoutedEventArgs e)
        {
            if (DaysListBox.SelectedItem is ListBoxItem selectedItem)
            {
                string selectedDay = selectedItem.Content.ToString();

                // Validate input times
                if (TimeOnly.TryParse(StartTimeTextBox.Text, out TimeOnly startTime) &&
                    TimeOnly.TryParse(FinishTimeTextBox.Text, out TimeOnly finishTime))
                {
                    // Add new shift to the data
                    ShiftsData[selectedDay].Add(new Shift ((Days)Enum.Parse(typeof(Days), selectedDay), startTime, finishTime));
                    // Update the UI
                    UpdateShiftsListView(selectedDay);

                    // Clear input fields
                    StartTimeTextBox.Clear();
                    FinishTimeTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Please enter valid times for the shift.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void DeleteShiftButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton && deleteButton.Tag is Shift shift)
            {
                if (DaysListBox.SelectedItem is ListBoxItem selectedItem)
                {
                    string selectedDay = selectedItem.Content.ToString();

                    // Remove the shift
                    ShiftsData[selectedDay].Remove(shift);

                    // Update the UI
                    UpdateShiftsListView(selectedDay);
                }
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krema_Roster_Maker.Model
{
    public class Staff
    {
        public string Name { get; set; }
        public Positions Position { get; set; }
        public WorkType WorkType { get; set; }

        public List<Shift> Shifts { get; private set; } = new List<Shift>();
        public Availability Availability { get; set; } = new Availability();

        public Staff(string name, Availability availability, Positions position, WorkType workType)
        {
            Name = name;
            Availability = availability;
            Position = position;
            WorkType = workType;
        }

        // Check if a shift is within availability
        public bool IsAvailableForShift(Shift shift)
        {
            return Availability.IsShiftWithinAvailability(shift);
        }

        // Add a shift to the staff
        public bool AddShift(Shift shift)
        {
            if (IsAvailableForShift(shift))
            {
                Shifts.Add(shift);
                return true;
            }
            else
            {
                Console.WriteLine($"Error: {Name} is not available for this shift.");
                return false;
            }
        }
    }
}

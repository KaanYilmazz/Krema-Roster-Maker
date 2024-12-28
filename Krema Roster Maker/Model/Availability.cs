using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krema_Roster_Maker.Model
{
    public class Availability
    {
        private Dictionary<Days, (TimeOnly Start, TimeOnly End)> DayAvailability { get; set; } = new();

        // Define availability for a specific day
        public void SetAvailability(Days day, TimeOnly startTime, TimeOnly endTime)
        {
            if (endTime <= startTime)
                throw new ArgumentException("End time must be after start time.");

            DayAvailability[day] = (startTime, endTime);
        }

        // Check if a shift is within availability
        public bool IsShiftWithinAvailability(Shift shift)
        {
            if (DayAvailability.TryGetValue(shift.Day, out var availability))
            {
                return shift.StartTime >= availability.Start && shift.FinishTime <= availability.End;
            }
            return false; // No availability set for the day
        }
    }
}

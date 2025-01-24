using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krema_Roster_Maker.Model
{
    public class Availability
    {
        public int? Id { get; set; }
        public List<DayAvailability> dayAvailabilityList { get; set; }
        //public ObservableCollection<DayAvailability> dayAvailabilityListOb { get; set; }

        public Availability()
        {
            dayAvailabilityList =
            [
                new DayAvailability(Days.Monday),
                new DayAvailability(Days.Tuesday),
                new DayAvailability(Days.Wednesday),
                new DayAvailability(Days.Thursday),
                new DayAvailability(Days.Friday),
                new DayAvailability(Days.Saturday),
                new DayAvailability(Days.Sunday),
            ];
        }
        // Define availability for a specific day
        public void SetAvailability(Days day, TimeOnly startTime, TimeOnly endTime)
        {
            var da = dayAvailabilityList.Find(x => x.Day == day);
            if (da != null)
            {
                if (endTime <= startTime)
                    throw new ArgumentException("End time must be after start time!");
               
                da.Start = startTime;
                da.End = endTime;
            }
            else
            {
                throw new ArgumentException("Day can not bee found!");
            }

        }
        public void SetAvailability(Days day, bool isFullTime, bool isOff)
        {
            var da = dayAvailabilityList.Find(x => x.Day == day);
            if (da != null)
            {
               
                if (isFullTime)
                {
                    da.IsFullTime = true;                  
                    return;
                }
                if (isOff)
                {
                    da.IsOff = true;    
                    return;
                }              
            }
            else
            {
                throw new ArgumentException("Day can not bee found!");
            }

        }

        // Check if a shift is within availability
        public bool IsShiftWithinAvailability(Shift shift)
        {
            var da = dayAvailabilityList.Find(x => x.Day == shift.Day);
            if (da != null)
            {
                if (da.IsFullTime)
                    return true;
                if (da.IsOff)
                    return false;

                return shift.StartTime >= da.Start && shift.FinishTime <= da.End;
            }
            return false; // No availability set for the day
        }
    }
}

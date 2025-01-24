using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krema_Roster_Maker.Model
{
    public class Shift
    {
        public int? Id { get; set; }
        public Days Day { get; set; }
        public bool IsOff { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly FinishTime { get; set; }
        public string  ActualShift {
            get
            {
               if (IsOff)
                {
                    return "OFF";
                }
                return StartTime.ToString()+" - " + FinishTime.ToString();
            }
             }
        public double ActualWork
        {
            get
            {
                TimeSpan workedTime = FinishTime - StartTime;
                double totalHours = workedTime.TotalHours;

                // Deduct breaks
                return totalHours > 8 ? totalHours - 0.5 : totalHours - 0.25;
            }
        }

        public Shift(Days day, TimeOnly startTime, TimeOnly finishTime)
        {
            if (finishTime <= startTime)
                throw new ArgumentException("Finish time must be after start time.");

            Day = day;
            StartTime = startTime;
            FinishTime = finishTime;
        }
    }
}

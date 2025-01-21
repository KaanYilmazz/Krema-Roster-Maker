using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krema_Roster_Maker.Model
{
    public class DayAvailability
    {

        public Days Day { get; set; }
        public bool IsFullTime { get; set; }
        public bool IsOff { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public DayAvailability(Days day)
        {
            Day = day;
        }
    }
}

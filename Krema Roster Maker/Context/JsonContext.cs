using Krema_Roster_Maker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krema_Roster_Maker.Context
{
   public  class JsonContext
    {
        public  List<Staff> Staffs { get; set; } = new List<Staff>();
        public  List<Shift> Shifts { get; set; } = new List<Shift>();
        public  List<DayAvailability> DayAvailabilities { get; set; } = new List<DayAvailability>();

    }
}

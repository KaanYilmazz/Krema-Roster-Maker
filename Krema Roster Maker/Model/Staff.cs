
namespace Krema_Roster_Maker.Model
{
    public class Staff
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public Positions Position { get; set; }
        public WorkType WorkType { get; set; }
       
        public List<Shift> Shifts { get; private set; } = new List<Shift>();
        public Availability Availability { get; set; } = new Availability();
        public double NetWork
        {
            get
            {
                double temp = 0;
                foreach (var item in Shifts)
                {

                    temp += item.ActualWork;
                }
                return temp;
            }
        }
        public Staff()
        {
               
        }
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

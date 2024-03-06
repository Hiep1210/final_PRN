using System;
using System.Collections.Generic;

namespace Final_Project_PRN.Models
{
    public partial class Room
    {
        public Room()
        {
            Schedules = new HashSet<Schedule>();
        }

        public string RoomName { get; set; } = null!;

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}

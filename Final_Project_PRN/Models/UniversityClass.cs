using System;
using System.Collections.Generic;

namespace Final_Project_PRN.Models
{
    public partial class UniversityClass
    {
        public UniversityClass()
        {
            Schedules = new HashSet<Schedule>();
        }

        public string ClassId { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}

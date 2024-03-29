﻿using System;
using System.Collections.Generic;

namespace Final_Project_PRN.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Schedules = new HashSet<Schedule>();
        }

        public string SubjectId { get; set; } = null!;
        public string SubjectName { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}

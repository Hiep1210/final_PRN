using System;
using System.Collections.Generic;

namespace Final_Project_PRN.Models
{
    public partial class TimeSlot
    {
        public int SlotTimeId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}

namespace Final_Project_PRN.Pages.Common
{
    public class ScheduleDTO
    {
        public string Class { get; set; }
        public string Subject { get; set; }
        public string Teacher { get; set; }
        public string Room { get; set; }
        public string SlotId { get; set; }

        public ScheduleDTO() { }
        public ScheduleDTO(string @class, string subject, string teacher, string room, string slotId)
        {
            Class = @class;
            Subject = subject;
            Teacher = teacher;
            Room = room;
            SlotId = slotId;
        }
    }
}

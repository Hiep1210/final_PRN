namespace Schedule_Project.DTOs
{
    public class CourseDTO
    {
        public CourseDTO() { }
        public string ClassId { get; set; }
        public string SubjectId { get; set; }

        public string Room { get; set; }

        public string Teacher { get; set; }

        public string SlotId { get; set; }
    }

    public class ScheduleListDTO
    {
        public ScheduleListDTO() { }

        public List<CourseDTO> ListOfScheduleInformation { get; set; }
    }
}

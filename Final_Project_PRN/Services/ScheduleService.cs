using Final_Project_PRN.Models;
using Final_Project_PRN.Pages.Common;

namespace Final_Project_PRN.Services
{
    public class ScheduleService
    {
        private readonly string semester = "Spring";
        private readonly int year = 2024;
        private readonly DateTime endDate = new DateTime(2024, 03, 23);
        private readonly DateTime startDate = new DateTime(2024, 01, 02);

        private readonly PRN221ProjectContext _context;
        public List<Schedule> Schedules { get; set; }
        public ScheduleService(PRN221ProjectContext context)
        {
            _context = context;
            Schedules = new List<Schedule>();
        }
        public List<Schedule> GetSchedules()
        {
            Schedules = _context.Schedules.ToList();
            return Schedules;
        }
        public void CreateSchedule(ScheduleDTO scheduleDTO)
        {
            Schedule scheduleToSave = MapSchedule(scheduleDTO);
            Schedules.Add(scheduleToSave);
            _context.Schedules.Add(scheduleToSave);
            _context.SaveChanges();
        }
        private Schedule MapSchedule(ScheduleDTO scheduleDTO)
        {
            Schedule schedule1 = new Schedule();
            schedule1.ClassId = scheduleDTO.Class;
            schedule1.Room = scheduleDTO.Room;
            schedule1.Teacher = scheduleDTO.Teacher;
            schedule1.SubjectId = scheduleDTO.Subject;
            schedule1.SlotId = scheduleDTO.SlotId;
            char[] breakSche = scheduleDTO.SlotId.ToCharArray();
            schedule1.Slot1 = int.Parse(breakSche[1].ToString());
            schedule1.Slot2 = int.Parse(breakSche[2].ToString());
            schedule1.TypeOfSlot = GetTypeOfSlot(breakSche[0]);
            schedule1.StartDate = startDate;
            schedule1.EndDate = endDate;
            schedule1.Season = semester;
            schedule1.Year = year;
            schedule1.DateCreated = DateTime.Now;
            schedule1.HasSessionYet = false;
            return schedule1;
        }
        public string GetTypeOfSlot(char slot)
        {
            switch (slot)
            {
                case 'A':
                    return Constant.MORNING;
                case 'P':
                    return Constant.AFTERNOON;
                case 'E':
                    return Constant.EVENING;
                default:
                    return Constant.INVALID_TYPE_OF_SLOT;
            }
        }

    }
}

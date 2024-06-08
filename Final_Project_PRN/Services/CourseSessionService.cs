using Final_Project_PRN.Models;
using Final_Project_PRN.Pages.Common;

namespace Final_Project_PRN.Services
{
    public class CourseSessionService
    {
        private readonly PRN221ProjectContext con;
        private SubjectService subjectService;
        public CourseSessionService(PRN221ProjectContext con, SubjectService subjectService)
        {
            this.con = con;
            this.subjectService = subjectService;
        }
        public void ImportEachSession(Schedule schedule)
        {
            List<CourseSession> courseSessionsList = new List<CourseSession>();
            int numberOfSessions = subjectService.GetNumberOfSessionInSubject(schedule.SubjectId);
            //session start from 0
            for (int i = 0; i < numberOfSessions; i++)
            {
                DateTime d = schedule.StartDate;
                int dayOfWeek = (int)d.DayOfWeek + 1;
                int dayOfSlot;
                string typeOfSlot = schedule.TypeOfSlot;
                //week will be session number /2 + 1
                int week = i / 2 + 1;
                int slot;
                if (i % 2 == 0)
                {
                    dayOfSlot = schedule.Slot1;
                    slot = 1;
                }
                else
                {
                    dayOfSlot = schedule.Slot2;
                    slot = 2;
                }
                //khoang cach giua thu cua ngay bat dau va thu ngay hien tai
                int gap = dayOfSlot - dayOfWeek;
                DateTime sessionDate = d.AddDays(7 * (week - 1) + gap);
                //get true number of slots
                int sessionSlot = GetSlotInDay(slot, typeOfSlot);
                CourseSession cs = new(schedule.Id, i + 1, sessionDate, schedule.Teacher, schedule.Room, sessionSlot);
                courseSessionsList.Add(cs);

            }
            con.CourseSessions.AddRange(courseSessionsList);
            //update flag to show that schedule has populated sessions
            Schedule sc = con.Schedules.First(x => x.Id == schedule.Id);
            sc.HasSessionYet = true;
            con.SaveChanges();
        }
        private int GetSlotInDay(int slot, string typeOfSlot)
        {
            int sessionSlot = slot;
            if (typeOfSlot.Equals(Constant.AFTERNOON))
            {
                sessionSlot = slot + 2;
            }
            else if (typeOfSlot.Equals(Constant.EVENING))
            {
                sessionSlot = slot + 4;
            }
            return sessionSlot;
        }
    }
}

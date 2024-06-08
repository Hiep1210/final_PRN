using Final_Project_PRN.Pages.Common;
using Final_Project_PRN.Services;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Final_Project_PRN.Pages.Validate
{
    public class ValidateSchedule
    {
        private ScheduleDTO scheduleDTO;
        private TeacherServices TeacherServices;
        private SubjectService SubjectService;
        private UniversityClassService UniversityClassService;
        private ScheduleService ScheduleService;
        public ValidateSchedule(TeacherServices teacherServices, SubjectService subjectService, UniversityClassService uni, ScheduleService scheduleService)
        {
            TeacherServices = teacherServices;
            SubjectService = subjectService;
            UniversityClassService = uni;
            ScheduleService = scheduleService;
        }
        public string validate(ScheduleDTO ScheduleDTO)
        {
            scheduleDTO = ScheduleDTO;
            if (MatchRegex())
            {
                return "wrong format for class or room";
;            }
            if (ExistInfor())
            {
                return "Teacher or Subject does not exist";
            }
            if (DuplicateSchedule(scheduleDTO))
            {
                return "duplicate with another schedule";
            }
            return "Schedule OK";
       }
        private bool MatchRegex()
        {
            if (!Regex.IsMatch(scheduleDTO.Class, FormatRegex.CLASS_NAME)
                    || !Regex.IsMatch(scheduleDTO.Room, FormatRegex.ROOM_NAME))
            {
                return true;
            }

            return false;
        }
        private bool ExistInfor()
        {
            if (!TeacherServices.GetAllTeacherId().Contains(scheduleDTO.Teacher)
                    || !SubjectService.GetSubjectIdList().Contains(scheduleDTO.Subject)
                    || !UniversityClassService.GetClassIdLists().Contains(scheduleDTO.Class))
            {
                return true;
            }
            return false;
        }
        private bool DuplicateSchedule(ScheduleDTO scheduleDTO)
        {

            char[] slotInformations = scheduleDTO.SlotId.ToCharArray();
            string typeOfSlot = ScheduleService.GetTypeOfSlot(slotInformations[0]);
            int slot1 = slotInformations[1] - '0';
            int slot2 = slotInformations[2] - '0';

            foreach (var course in ScheduleService.GetSchedules())
            {
                if (course.ClassId.Equals(scheduleDTO.Class) && course.SubjectId.Equals(scheduleDTO.Subject))
                {
                    return true;
                }
                if (course.Room.Equals(scheduleDTO.Room) && course.TypeOfSlot.Equals(typeOfSlot) &&
                    (course.Slot1.Equals(slot1) || course.Slot2.Equals(slot2)))
                {
                    return true;
                }

                if (course.Teacher == scheduleDTO.Teacher && course.TypeOfSlot == typeOfSlot
                    && (course.Slot1 == slot1 || course.Slot2 == slot2))
                {
                    return true;
                }
                if (course.ClassId == scheduleDTO.Class && course.TypeOfSlot == typeOfSlot
                    && (course.Slot1 == slot1 || course.Slot2 == slot2)
                    && (course.Teacher != scheduleDTO.Teacher || course.Room != scheduleDTO.Room))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

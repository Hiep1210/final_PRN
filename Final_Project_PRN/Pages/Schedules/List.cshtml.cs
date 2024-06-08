using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final_Project_PRN.Models;
using Final_Project_PRN.Pages.Common;
using Final_Project_PRN.Pages.Validate;
using Microsoft.Extensions.Hosting;
using static Final_Project_PRN.Pages.Common.EnumPrototype;
using System.Formats.Asn1;
using System.IO;
using System.Globalization;
using CsvHelper;
using System.Text.Json;
using Final_Project_PRN.Services;
using Schedule_Project.DTOs;

namespace Final_Project_PRN.Pages.Schedules
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private ScheduleService ScheduleService;
        private TeacherServices TeacherServices;
        private SubjectService SubjectService;
        private UniversityClassService UniversityClassService;
        private CourseSessionService CourseSessionService;
        private ValidateSchedule ValidateSchedule;

        private readonly Final_Project_PRN.Models.PRN221ProjectContext _context;

        public List<ScheduleDTO> ScheduleDTO { get; set; }
        public Dictionary<int, Tuple<ScheduleDTO,string>> ImportStatus = new Dictionary<int, Tuple<ScheduleDTO, string>>();
        public Dictionary<Tuple<string, int>, DisplayCell> dictionary = new Dictionary<Tuple<string, int>, DisplayCell>();
        public DateTime SelectedDate { get; set; }
        [BindProperty]
        public IFormFile FileUpload { get; set; }
        public IndexModel(Final_Project_PRN.Models.PRN221ProjectContext context, IWebHostEnvironment environment,
            ScheduleService scheduleService, TeacherServices teacherServices, SubjectService subjectService, UniversityClassService uni, CourseSessionService courseSessionService, ValidateSchedule validateSchedule)
        {
            _context = context;
            _environment = environment;
            ScheduleService = scheduleService;
            TeacherServices = teacherServices;
            SubjectService = subjectService;
            UniversityClassService = uni;
            CourseSessionService = courseSessionService;
            ValidateSchedule = validateSchedule;
        }
        public HashSet<String> RoomInSchedule { get; set; } = default!;
        public void OnGet()
        {
            SelectedDate = DateTime.Now;
            GetListSession(SelectedDate);
        }

        public void OnPostDate(DateTime dadate)
        {
            SelectedDate = dadate;
            GetListSession(SelectedDate);
        }

        public IActionResult OnPostImport()
        {
            string message = ValidateFile.validate(FileUpload);
            if(!message.Equals("Import OK"))
            {
                TempData["Message"] = message;
                return Page();
            }
            ScheduleDTO = SaveFile();
            validateScheList(ScheduleDTO);
            PopulateSessions();
            TempData["Result"] = JsonSerializer.Serialize(ImportStatus);
            return RedirectToPage("/Import/ImportResult");
        }
        private void PopulateSessions()
        {
            foreach(Schedule schedule in ScheduleService.Schedules)
            {
                //chua co session thi populate
                if(!schedule.HasSessionYet)
                {
                    CourseSessionService.ImportEachSession(schedule);
                }
            }
        }
        private void validateScheList(List<ScheduleDTO> dtos)
        {
            int ind = 0;
            foreach (ScheduleDTO dto in dtos)
            {
                Tuple<ScheduleDTO, string> value = Tuple.Create(dto, ValidateSchedule.validate(dto));
                ImportStatus.Add(ind, value);
                //add dto to list of schedule
                if(value.Item2.Equals("Schedule OK"))
                ScheduleService.CreateSchedule(dto);
                ind++;
            }
        }
        private List<ScheduleDTO> SaveFile()
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "fileuploaded");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, FileUpload.FileName);
            string fileType = Path.GetExtension(FileUpload.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                FileUpload.CopyTo(stream);
            }
            return FileToScheduleDTO(filePath, fileType);
        }
        private List<ScheduleDTO> FileToScheduleDTO(string filePath, string fileType)
        {
            List<ScheduleDTO> scheduleDTOs;
            switch (fileType)
            {
                case ".json":
                    scheduleDTOs = DeserializeToJSON(filePath);
                    break;
                case ".csv":
                    scheduleDTOs = DeserialiseCSV(filePath);
                    break;
                case ".xml":
                default:
                    scheduleDTOs = new List<ScheduleDTO>();
                    break;
            }

            return scheduleDTOs;
        }

        private List<ScheduleDTO> DeserializeToJSON(string filePath)
        {
            string json = System.IO.File.ReadAllText(filePath);
            List<ScheduleDTO> scheduleDTOs = JsonSerializer.Deserialize<List<ScheduleDTO>>(json);
            return scheduleDTOs;
        }

        private List<ScheduleDTO> DeserialiseCSV(string filePath)
        {
            var scheduleDTOs = new List<ScheduleDTO>();
            if (!System.IO.File.Exists(filePath)) return new List<ScheduleDTO>();
            var reader = new StreamReader(filePath);
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                scheduleDTOs = csv.GetRecords<ScheduleDTO>().ToList();
            }
            return scheduleDTOs.ToList();
        }
        private void GetListSession(DateTime SelectedDate)
        {
            var Session = _context.CourseSessions.Include(courseSession => courseSession.Course).Where(d => d.SessionDate == SelectedDate).ToList();
            RoomInSchedule = Session.Select(x => x.Room).ToHashSet();
            foreach (var session in Session)
            {
                var key = new Tuple<string, int>(session.Room, session.Slot);
                DisplayCell value = new(0,session.Course.ClassId, session.Course.SubjectId, session.Course.Teacher);
                dictionary.Add(key, value);
            }
        }


    }
}

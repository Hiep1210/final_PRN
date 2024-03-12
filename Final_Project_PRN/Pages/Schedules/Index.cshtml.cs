using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final_Project_PRN.Models;
using Final_Project_PRN.Pages.Common;

namespace Final_Project_PRN.Pages.Schedules
{
    public class IndexModel : PageModel
    {
        public Dictionary<Tuple<string, int>, DisplayCell> dictionary = new Dictionary<Tuple<string, int>, DisplayCell>();
        private readonly Final_Project_PRN.Models.PRN221ProjectContext _context;
        public DateTime selectedDate {  get; set; }
        public IndexModel(Final_Project_PRN.Models.PRN221ProjectContext context)
        {
            _context = context;
        }

        public List<Schedule> Schedule { get;set; } = default!;

        public List<String> RoomInSchedule { get; set; } = default!;

        public void OnGet()
        {
            selectedDate = DateTime.Now;
            GetListSchedule(selectedDate);
        }

        public void OnPostDate(DateTime dadate)
        {
            selectedDate = dadate;
            GetListSchedule(selectedDate);
        }

        private void GetListSchedule(DateTime date)
        {
            int? dayOfWeek = (int?)date.DayOfWeek + 1;
            //default is today
            Schedule = _context.Schedules.Where(x => (date >= x.DateStarted) && (date <= x.DateEnd)).Where(x => (x.Slot1 == dayOfWeek) || (x.Slot2 == dayOfWeek)).ToList();
            RoomInSchedule = Schedule.GroupBy(x => x.Room).Select(x => x.Key).ToList();
            int slot = 0;
            foreach (Schedule schedule in Schedule)
            {
                if(schedule.Morning)
                {
                    if(schedule.Slot1 == dayOfWeek)
                    {
                        slot = 1;
                    }else if(schedule.Slot2 == dayOfWeek){
                        slot = 2;
                    }
                }
                else
                {
                    if (schedule.Slot1 == dayOfWeek)
                    {
                        slot = 3;
                    }
                    else if (schedule.Slot2 == dayOfWeek)
                    {
                        slot = 4;
                    }
                }
                var key = new Tuple<string, int>(schedule.Room, slot);
                DisplayCell value = new DisplayCell(schedule.ClassId, schedule.SubjectId, schedule.Teacher);
                dictionary.Add(key, value);
            }
        }
    }
}

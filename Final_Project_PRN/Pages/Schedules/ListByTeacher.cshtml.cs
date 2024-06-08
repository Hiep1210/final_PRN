using Final_Project_PRN.Models;
using Final_Project_PRN.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Final_Project_PRN.Pages.Schedules
{
    public class ListByTeacher : PageModel
    {
        private DateTime endDate = new DateTime();
        private DateTime startDate = new DateTime(2024, 01, 02);
        private PRN221ProjectContext con;
        public Dictionary<Tuple<int, int>, DisplayCell> SessDict { get; set; } = new Dictionary<Tuple<int, int>, DisplayCell>();
        [BindProperty(SupportsGet = true)]
        public int week { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public string teacher { get; set; } = "BanTQ";
        public List<string> TeacherList { get; set; } = new List<string>();
        public ListByTeacher(PRN221ProjectContext con)
        {
            this.con = con;

        }
        public void OnGet()
        {

            startDate = startDate.AddDays((week - 1) * 7);
            endDate = startDate.AddDays(7);
            TeacherList = con.Teachers.Select(x => x.TeacherId).ToList();
            LoadSchedDict();
        }
        private void LoadSchedDict()
        {
            var Session = con.CourseSessions.Include(courseSession => courseSession.Course).
                Where(d => d.SessionDate >= startDate && d.SessionDate < endDate)
                .Where(x => x.Teacher.Equals(teacher)).ToList();
            SessDict.Clear();
            foreach (var session in Session)
            {
                var key = Tuple.Create((int)session.SessionDate.DayOfWeek + 1, session.Slot);
                DisplayCell value = new(session.Id, session.Course.ClassId, session.Course.SubjectId, session.Course.Teacher);
                SessDict.Add(key, value);
            }
        }
    }
}

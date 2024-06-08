using Final_Project_PRN.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Final_Project_PRN.Pages.Import
{
    public class ImportResultModel : PageModel
    {
        public Dictionary<int, Tuple<ScheduleDTO, string>> ImportStatus = new Dictionary<int, Tuple<ScheduleDTO, string>>();
        public void OnGet()
        {
            var result = TempData["Result"].ToString();
            Dictionary<int, Tuple<ScheduleDTO, string>>? courseResults = JsonSerializer.Deserialize<Dictionary<int, Tuple<ScheduleDTO, string>>>(result);
            if (courseResults != null)
            {
                ImportStatus = courseResults;
            }

        }
    }
}

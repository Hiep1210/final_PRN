using Final_Project_PRN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project_PRN.Pages.Schedule
{
    public class ListModel : PageModel
    {
        public readonly PRN221ProjectContext context;
        public ListModel(PRN221ProjectContext context)
        {
            this.context = context;
        }
        public void OnGet()
        {
        }
    }
}

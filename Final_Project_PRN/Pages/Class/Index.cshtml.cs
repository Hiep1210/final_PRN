using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final_Project_PRN.Models;

namespace Final_Project_PRN.Pages.Class
{
    public class IndexModel : PageModel
    {
        private readonly Final_Project_PRN.Models.PRN221ProjectContext _context;

        public IndexModel(Final_Project_PRN.Models.PRN221ProjectContext context)
        {
            _context = context;
        }

        public IList<UniversityClass> UniversityClass { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.UniversityClasses != null)
            {
                UniversityClass = await _context.UniversityClasses.ToListAsync();
            }
        }
    }
}

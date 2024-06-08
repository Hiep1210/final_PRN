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
    public class DeleteModel : PageModel
    {
        private readonly Final_Project_PRN.Models.PRN221ProjectContext _context;

        public DeleteModel(Final_Project_PRN.Models.PRN221ProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
      public UniversityClass UniversityClass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.UniversityClasses == null)
            {
                return NotFound();
            }

            var universityclass = await _context.UniversityClasses.FirstOrDefaultAsync(m => m.ClassId == id);

            if (universityclass == null)
            {
                return NotFound();
            }
            else 
            {
                UniversityClass = universityclass;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.UniversityClasses == null)
            {
                return NotFound();
            }
            var universityclass = await _context.UniversityClasses.FindAsync(id);

            if (universityclass != null)
            {
                UniversityClass = universityclass;
                _context.UniversityClasses.Remove(UniversityClass);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

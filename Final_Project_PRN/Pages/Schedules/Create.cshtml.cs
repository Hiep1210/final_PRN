using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Final_Project_PRN.Models;

namespace Final_Project_PRN.Pages.Schedules
{
    public class CreateModel : PageModel
    {
        private readonly Final_Project_PRN.Models.PRN221ProjectContext _context;

        public CreateModel(Final_Project_PRN.Models.PRN221ProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClassId"] = new SelectList(_context.UniversityClasses, "ClassId", "ClassId");
        ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
        ViewData["Teacher"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId");
            return Page();
        }

        [BindProperty]
        public Schedule Schedule { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Schedules == null || Schedule == null)
            {
                return Page();
            }

            _context.Schedules.Add(Schedule);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

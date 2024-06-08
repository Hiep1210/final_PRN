using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_Project_PRN.Models;

namespace Final_Project_PRN.Pages.Class
{
    public class EditModel : PageModel
    {
        private readonly Final_Project_PRN.Models.PRN221ProjectContext _context;

        public EditModel(Final_Project_PRN.Models.PRN221ProjectContext context)
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

            var universityclass =  await _context.UniversityClasses.FirstOrDefaultAsync(m => m.ClassId == id);
            if (universityclass == null)
            {
                return NotFound();
            }
            UniversityClass = universityclass;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UniversityClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UniversityClassExists(UniversityClass.ClassId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UniversityClassExists(string id)
        {
          return (_context.UniversityClasses?.Any(e => e.ClassId == id)).GetValueOrDefault();
        }
    }
}

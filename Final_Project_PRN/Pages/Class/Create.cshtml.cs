using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Final_Project_PRN.Models;

namespace Final_Project_PRN.Pages.Class
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
            return Page();
        }

        [BindProperty]
        public UniversityClass UniversityClass { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.UniversityClasses == null || UniversityClass == null)
            {
                return Page();
            }

            _context.UniversityClasses.Add(UniversityClass);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

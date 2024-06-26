﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final_Project_PRN.Models;

namespace Final_Project_PRN.Pages.Schedules
{
    public class DeleteModel : PageModel
    {
        private readonly Final_Project_PRN.Models.PRN221ProjectContext _context;

        public DeleteModel(Final_Project_PRN.Models.PRN221ProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Schedule Schedule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FirstOrDefaultAsync(m => m.Id == id);

            if (schedule == null)
            {
                return NotFound();
            }
            else 
            {
                Schedule = schedule;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }
            var schedule = await _context.Schedules.FindAsync(id);

            if (schedule != null)
            {
                Schedule = schedule;
                _context.Schedules.Remove(Schedule);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

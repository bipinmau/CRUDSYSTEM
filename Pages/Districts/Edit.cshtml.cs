using DRDOAssignment.Data;
using DRDOAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DRDOAssignment.Pages.Districts
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public District District { get; set; }

        public SelectList States { get; set; }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            District = await _context.Districts.FirstOrDefaultAsync(m => m.Id == id);

            if (District == null)
            {
                return NotFound();
            }

            States = new SelectList(_context.States, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                States = new SelectList(_context.States, "Id", "Name");
                return Page();
            }

            _context.Attach(District).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistrictExists(District.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["SuccessMessage"] = "District updated successfully.";
            return RedirectToPage("./Index");
        }

        private bool DistrictExists(short id)
        {
            return _context.Districts.Any(e => e.Id == id);
        }
    }
} 
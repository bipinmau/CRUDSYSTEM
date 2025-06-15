using DRDOAssignment.Data;
using DRDOAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DRDOAssignment.Pages.Genders
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
        public Gender Gender { get; set; }

        public async Task<IActionResult> OnGetAsync(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Gender = await _context.Genders.FirstOrDefaultAsync(m => m.Id == id);

            if (Gender == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Gender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderExists(Gender.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["SuccessMessage"] = "Gender updated successfully.";
            return RedirectToPage("./Index");
        }

        private bool GenderExists(byte id)
        {
            return _context.Genders.Any(e => e.Id == id);
        }
    }
} 
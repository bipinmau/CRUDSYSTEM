using DRDOAssignment.Data;
using DRDOAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DRDOAssignment.Pages.Districts
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public District District { get; set; }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            District = await _context.Districts.Include(d => d.State).FirstOrDefaultAsync(m => m.Id == id);

            if (District == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            District = await _context.Districts.FindAsync(id);

            if (District != null)
            {
                _context.Districts.Remove(District);
                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = "District deleted successfully.";
            return RedirectToPage("./Index");
        }
    }
} 
using DRDOAssignment.Data;
using DRDOAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DRDOAssignment.Pages.Genders
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

        public async Task<IActionResult> OnPostAsync(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Gender = await _context.Genders.FindAsync(id);

            if (Gender != null)
            {
                _context.Genders.Remove(Gender);
                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = "Gender deleted successfully.";
            return RedirectToPage("./Index");
        }
    }
} 
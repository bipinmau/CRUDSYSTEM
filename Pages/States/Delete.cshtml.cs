using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DRDOAssignment.Data;
using DRDOAssignment.Models;

namespace DRDOAssignment.Pages.States
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public State State { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .Include(s => s.Districts)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (state == null)
            {
                return NotFound();
            }

            if (state.Districts.Any())
            {
                ModelState.AddModelError("", "Cannot delete state because it has associated districts.");
                return RedirectToPage("./Index");
            }

            State = state;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .Include(s => s.Districts)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (state == null)
            {
                return NotFound();
            }

            if (state.Districts.Any())
            {
                ModelState.AddModelError("", "Cannot delete state because it has associated districts.");
                return RedirectToPage("./Index");
            }

            State = state;
            _context.States.Remove(State);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
} 
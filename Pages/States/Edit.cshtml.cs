using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DRDOAssignment.Data;
using DRDOAssignment.Models;

namespace DRDOAssignment.Pages.States
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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

            var state = await _context.States.FirstOrDefaultAsync(m => m.Id == id);
            if (state == null)
            {
                return NotFound();
            }
            State = state;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(State).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(State.Id))
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

        private bool StateExists(short id)
        {
            return _context.States.Any(e => e.Id == id);
        }
    }
} 
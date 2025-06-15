using DRDOAssignment.Data;
using DRDOAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DRDOAssignment.Pages.CustomerInfos
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
        public CustomerInfo Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.CustomerInfos
                .Include(c => c.Gender)
                .Include(c => c.District)
                .ThenInclude(d => d.State)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Customer == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.CustomerInfos.FindAsync(id);

            if (Customer != null)
            {
                _context.CustomerInfos.Remove(Customer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
} 
using DRDOAssignment.Data;
using DRDOAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DRDOAssignment.Pages.CustomerInfos
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
        public CustomerInfo Customer { get; set; }

        public SelectList GenderList { get; set; }
        public SelectList StateList { get; set; }
        public short SelectedStateId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.CustomerInfos
                .Include(c => c.District)
                    .ThenInclude(d => d.State)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Customer == null)
            {
                return NotFound();
            }

            // Get the state ID from the district
            SelectedStateId = Customer.District?.StateId ?? 0;

            GenderList = new SelectList(await _context.Genders.ToListAsync(), "Id", "Name");
            StateList = new SelectList(await _context.States.ToListAsync(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                GenderList = new SelectList(await _context.Genders.ToListAsync(), "Id", "Name");
                StateList = new SelectList(await _context.States.ToListAsync(), "Id", "Name");
                return Page();
            }

            var existingCustomer = await _context.CustomerInfos.FindAsync(Customer.Id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.Name = Customer.Name;
            existingCustomer.GenderId = Customer.GenderId;
            existingCustomer.DistrictId = Customer.DistrictId;

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool CustomerExists(int id)
        {
            return _context.CustomerInfos.Any(e => e.Id == id);
        }

        public async Task<IActionResult> OnGetGetDistrictsAsync(int stateId)
        {
            var districts = await _context.Districts
                .Where(d => d.StateId == stateId)
                .Select(d => new { id = d.Id, name = d.Name })
                .ToListAsync();
            return new JsonResult(districts);
        }
    }
} 
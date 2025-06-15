using DRDOAssignment.Data;
using DRDOAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DRDOAssignment.Pages.CustomerInfos
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CustomerInfo Customer { get; set; }

        public SelectList GenderList { get; set; }
        public SelectList StateList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
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

            _context.CustomerInfos.Add(Customer);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<JsonResult> OnGetGetDistrictsAsync(int stateId)
        {
            var districts = await _context.Districts
                .Where(d => d.StateId == stateId)
                .Select(d => new { id = d.Id, name = d.Name })
                .ToListAsync();
            return new JsonResult(districts);
        }
    }
} 
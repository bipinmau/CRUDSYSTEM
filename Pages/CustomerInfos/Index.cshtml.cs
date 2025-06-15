using DRDOAssignment.Data;
using DRDOAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DRDOAssignment.Pages.CustomerInfos
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CustomerViewModel> Customers { get; set; }

        public class CustomerViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string GenderName { get; set; }
            public string StateName { get; set; }
            public string DistrictName { get; set; }
        }

        public async Task OnGetAsync()
        {
            Customers = await _context.CustomerInfos
                .Include(c => c.Gender)
                .Include(c => c.District)
                    .ThenInclude(d => d.State)
                .Select(c => new CustomerViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    GenderName = c.Gender.Name,
                    StateName = c.District.State.Name,
                    DistrictName = c.District.Name
                })
                .ToListAsync();
        }
    }
} 
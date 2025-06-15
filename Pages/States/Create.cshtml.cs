using DRDOAssignment.Data;
using DRDOAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DRDOAssignment.Pages.States
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ApplicationDbContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public State State { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state when creating state");
                return Page();
            }

            try
            {
                _context.States.Add(State);
                await _context.SaveChangesAsync();
                _logger.LogInformation("State created successfully: {StateName}", State.Name);
                TempData["SuccessMessage"] = "State created successfully!";
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error occurred while saving state to database");
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return Page();
            }
        }
    }
} 
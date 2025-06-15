using DRDOAssignment.Data;
using DRDOAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DRDOAssignment.Pages.Districts
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
            try
            {
                States = new SelectList(_context.States, "Id", "Name");
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading states for district creation");
                TempData["ErrorMessage"] = "Error loading states. Please try again.";
                return RedirectToPage("./Index");
            }
        }

        [BindProperty]
        public District District { get; set; }

        public SelectList States { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state when creating district");
                    States = new SelectList(_context.States, "Id", "Name");
                    return Page();
                }

                // Verify that the selected state exists
                var stateExists = await _context.States.AnyAsync(s => s.Id == District.StateId);
                if (!stateExists)
                {
                    ModelState.AddModelError("District.StateId", "Selected state does not exist.");
                    States = new SelectList(_context.States, "Id", "Name");
                    return Page();
                }

                // Verify that District.Name is not null or empty
                if (string.IsNullOrEmpty(District.Name))
                {
                    ModelState.AddModelError("District.Name", "District name is required.");
                    _logger.LogWarning("District name is null or empty");
                    States = new SelectList(_context.States, "Id", "Name");
                    return Page();
                }

                _context.Districts.Add(District);
                await _context.SaveChangesAsync();

                _logger.LogInformation("District created successfully: {DistrictName}", District.Name);
                TempData["SuccessMessage"] = "District created successfully.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating district");
                TempData["ErrorMessage"] = "An error occurred while creating the district. Please try again.";
                States = new SelectList(_context.States, "Id", "Name");
                return Page();
            }
        }
    }
} 
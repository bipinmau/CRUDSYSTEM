using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DRDOAssignment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DRDOAssignment.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(10, ErrorMessage = "DomainName must be 10 characters or less.")]
            [RegularExpression("^[a-zA-Z0-9_]{1,10}$", ErrorMessage = "DomainName can only contain letters, numbers, and underscores.")]
            public string DomainName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please check the form for errors.";
                return Page();
            }

            var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, DomainName = Input.DomainName };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                TempData["SuccessMessage"] = "Registration successful! You are now logged in.";
                return RedirectToPage("/Index");
            }
            foreach (var error in result.Errors)
            {
                if (error.Code == "DuplicateUserName")
                {
                    TempData["ErrorMessage"] = "This email is already registered. Please use a different email.";
                }
                else
                {
                    TempData["ErrorMessage"] = error.Description;
                }
            }
            return Page();
        }
    }
} 
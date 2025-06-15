using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DRDOAssignment.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(10)]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "DomainName can only contain letters, numbers, and underscore")]
        public string DomainName { get; set; }= string.Empty;
    }
} 
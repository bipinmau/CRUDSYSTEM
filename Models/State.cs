using System.ComponentModel.DataAnnotations;

namespace DRDOAssignment.Models
{
    public class State
    {
        public short Id { get; set; }

        [Required(ErrorMessage = "State name is required")]
        [StringLength(50, ErrorMessage = "State name cannot be longer than 50 characters")]
        public string Name { get; set; } = string.Empty;

        public ICollection<District> Districts { get; set; } = new List<District>();
    }
} 
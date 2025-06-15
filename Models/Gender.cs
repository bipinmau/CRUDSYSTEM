using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRDOAssignment.Models
{
    public class Gender
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [Required(ErrorMessage = "Gender name is required")]
        [StringLength(50, ErrorMessage = "Gender name cannot be longer than 50 characters")]
        public string Name { get; set; } = string.Empty;
    }
} 
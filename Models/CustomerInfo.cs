using System.ComponentModel.DataAnnotations;

namespace DRDOAssignment.Models
{
    public class CustomerInfo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public byte GenderId { get; set; }
        public Gender Gender { get; set; } = null!;

        public short DistrictId { get; set; }
        public District District { get; set; } = null!;
    }

    public class CustomerInfosave
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public byte GenderId { get; set; }
        [Required]
        public short DistrictId { get; set; }
        
    }
} 
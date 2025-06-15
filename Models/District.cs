using System.ComponentModel.DataAnnotations;

namespace DRDOAssignment.Models
{
    public class District
    {
        public short Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public short StateId { get; set; }
        public State State { get; set; }
    }

    #region View Model for listing and single retrieval
    public class DistrictViewModel
    {
        public short Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public short StateId { get; set; }
        public string StateName { get; set; } = string.Empty;
    }
    #endregion

    #region Input model for create/update
    public class DistrictInputModel
    {
        public string Name { get; set; } = string.Empty;
        public short StateId { get; set; }
    }
    #endregion
}
namespace DRDOAssignment.DTO
{
    public class CustomerEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public byte GenderId { get; set; }
        public short DistrictId { get; set; }
        public short StateId { get; set; }  
    }

}

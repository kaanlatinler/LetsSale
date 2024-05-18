using System.ComponentModel.DataAnnotations;

namespace LetsSale.Models.Entities
{
    public class Employees
    {
        [Key]
        public int EmployeeID { get; set; }
        public Guid EMainID { get; set; }
        public required string EName { get; set; }
        public required string ELastName { get; set; }
        public required string EPhoneNumber { get; set; }
        public required string EEmail { get; set; }
        public required string EPassword { get; set; }
        public DateTime EStartDate { get; set; }
        public int SaledCarsID { get; set; }
        public int ERankID { get; set; }
    }
}

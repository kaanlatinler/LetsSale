using System.ComponentModel.DataAnnotations;

namespace LetsSale.Models.Entities
{
    public class Saled_Cars
    {
        [Key]
        public int SaledCarsID { get; set; }
        public Guid SCMainID { get; set; }
        public required string SCarName { get; set; }
        public required string SCarBrand { get; set; }
        public required string SCarPrice { get; set; }
        public required string SCarPlateNumber { get; set; }
        public required int SCarCategoryID { get; set; }
        public required string SCarMainPic { get; set; }
        public required Guid EmployeeID { get; set; }
        public required Guid UserId { get; set; }
        public required DateTime SCarSaleDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace LetsSale.Models.Entities
{
    public class User_Cars
    {
        [Key]
        public int CarID { get; set; }
        public Guid CMainID { get; set; }
        public required Guid UserID { get; set; }
        public required string CarName { get; set; }
        public required string CarBrand { get; set; }
        public required string CarPlateNumber { get; set; }
        public required int CarYear { get; set; }
        public required DateTime CarSaleDate { get; set; }
    }
}

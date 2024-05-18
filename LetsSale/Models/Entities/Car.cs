using System.ComponentModel.DataAnnotations;

namespace LetsSale.Models.Entities
{
    public class Car
    {
        [Key]
        public int CarsID { get; set; }
        public Guid CMainID { get; set; }
        public required string CarName { get; set; }
        public required string CarBrand { get; set; }
        public required int CarPrice { get; set; }
        public required string CarPlateNumber { get; set; }
        public required int CarYear { get; set; }
        public required string CarColor { get; set; }
        public required int CarHP { get; set; }
        public required int CarTorque { get; set; }
        public required int CarMaxFuelTankCapacity { get; set; }
        public required int CarMaxSpeed { get; set; }
        public required string CarTransmission { get; set; }
        public required int CarCargoVolume { get; set; }
        public string CarMainPic{ get; set; }
        public Guid CarPicsID { get; set; }
        public required int CarCategoryID { get; set; }
        public required Guid EmployeeID { get; set; }
    }
}

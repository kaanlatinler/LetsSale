namespace LetsSale.Models
{
    public class IndexCarsViewModel
    {
        public required string CarName { get; set; }
        public required string CarBrand { get; set; }
        public required string CarColor { get; set; }
        public required int CarHP { get; set; }
        public required int CarTorque { get; set; }
        public required int CarMaxFuelTankCapacity { get; set; }
        public required int CarMaxSpeed { get; set; }
        public required string CarTransmission { get; set; }
        public required int CarCargoVolume { get; set; }
        public string CarMainPic { get; set; }
    }
}

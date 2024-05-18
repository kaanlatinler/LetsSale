namespace LetsSale.Models
{
    public class UserCarsViewModel
    {
        public int CarID { get; set; }
        public Guid CMainID { get; set; }
        public  Guid UserID { get; set; }
        public string CarName { get; set; }
        public string CarBrand { get; set; }
        public string CarPlateNumber { get; set; }
        public int CarYear { get; set; }
        public DateTime CarSaleDate { get; set; }
    }
}

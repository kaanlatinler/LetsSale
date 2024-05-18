namespace LetsSale.Models
{
    public class SaledCarsViewModel
    {
        public int SaledCarsID { get; set; }
        public Guid SCMainID { get; set; }
        public string SCarName { get; set; }
        public string SCarBrand { get; set; }
        public string SCarPrice { get; set; }
        public string SCarPlateNumber { get; set; }
        public int SCarCategoryID { get; set; }
        public string SCarMainPic { get; set; }
        public Guid EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public Guid UserId { get; set; }
        public DateTime SCarSaleDate { get; set; }
    }
}

namespace LetsSale.Models
{
    public class ServiceCarsViewModel
    {
        public Guid SMainID { get; set; }
        public Guid SCarID { get; set; }
        public Guid SUserID { get; set; }
        public Guid SEmployeeID { get; set; }
        public string SDesc { get; set; }
        public DateTime SstartDate { get; set; }
        public DateTime SfinishDate { get; set; }
        public bool IsFinish { get; set; }
    }
}

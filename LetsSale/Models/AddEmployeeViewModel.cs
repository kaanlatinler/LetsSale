using Microsoft.AspNetCore.Mvc.Rendering;

namespace LetsSale.Models
{
    public class AddEmployeeViewModel
    {
        public Guid EMainID { get; set; }
        public string EName { get; set; }
        public string ELastName { get; set; }
        public string EPhoneNumber { get; set; }
        public string EEmail { get; set; }
        public string EPassword { get; set; }
        public DateTime EStartDate { get; set; }
        public int SaledCarsID { get; set; }
        public int ERankID { get; set; }
        public SelectList Ranks { get; set; }
    }
}

namespace LetsSale.Models
{
    public class UserDetailsViewModel
    {
        public Guid UMainID { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public DateTime UserRegisterDate { get; set; }
        public Guid UserCarsID { get; set; }
    }
}

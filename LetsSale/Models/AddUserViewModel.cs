namespace LetsSale.Models
{
    public class AddUserViewModel
    {
        public Guid UMainID { get; set; }
        public required string UserName { get; set; }
        public required string UserLastName { get; set; }
        public required string UserPhoneNumber { get; set; }
        public required string UserEmail { get; set; }
        public required string UserPassword { get; set; }
        public DateTime UserRegisterDate { get; set; }
    }
}

namespace LetsSale.Models.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public Guid UMainID { get; set; }
        public required string UserName { get; set; }
        public required string UserLastName { get; set; }
        public required string UserPhoneNumber { get; set; }
        public required string UserEmail { get; set; }
        public required string UserPassword { get; set; }
        public DateTime UserRegisterDate { get; set; }
        public Guid UserCarsID { get; set; }
    }
}

namespace LetsSale.Models
{
    public class NotifyViewModel
    {
        public int NotifyID { get; set; }
        public Guid CarID { get; set; }
        public Guid UserID { get; set; }
        public DateTime NotifyTime { get; set; }
        public bool NotifyType{ get; set; }
    }
}

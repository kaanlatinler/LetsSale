namespace LetsSale.Models
{
    public class PicturesViewModel
    {
        public int id { get; set; }
        public required string CarPics { get; set; }
        public required Guid CarID { get; set; }
    }
}

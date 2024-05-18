using System.ComponentModel.DataAnnotations;

namespace LetsSale.Models.Entities
{
    public class Car_Pics
    {
        [Key]
        public int id { get; set; }
        public required string CarPicsPath { get; set; }
        public Guid CarID { get; set; }
    }
}

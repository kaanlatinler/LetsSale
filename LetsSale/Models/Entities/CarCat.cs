using System.ComponentModel.DataAnnotations;

namespace LetsSale.Models.Entities
{
    public class CarCat
    {
        [Key]
        public int CatID { get; set; }
        public required string CatName { get; set; }
    }
}

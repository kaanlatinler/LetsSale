using System.ComponentModel.DataAnnotations;

namespace LetsSale.Models.Entities
{
    public class ERank
    {
        [Key]
        public int ERankID { get; set; }
        public required string ERankName { get; set; }
    }
}

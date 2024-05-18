using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LetsSale.Models.Entities
{
    public class Notifies
    {
        [Key]
        public int NotifyID { get; set; }
        public Guid CarID { get; set; }
        public Guid UserID { get; set; }
        public DateTime NotifyTime { get; set; }
        public bool NotifyType { get; set; }
    }
}

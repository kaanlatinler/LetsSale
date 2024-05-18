using Microsoft.AspNetCore.Mvc.Rendering;

namespace LetsSale.Models
{
    public class SaleCarViewModel
    {
        public CarDetailsViewModel CarDetails { get; set; }
        public UserDetailsViewModel UserDetails { get; set; }
    }
}

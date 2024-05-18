using LetsSale.Models.Entities;

namespace LetsSale.Models
{
    public class  CarDetailsViewModel
    {
        public CarsViewModel Cars { get; set; }
        public SaledCarsViewModel SaledCars { get; set; }
        public CategoriesViewModel Categories { get; set; }
        public List<PicturesViewModel> Pics { get; set; }
        public EmployeeDetailsViewModel EmployeeDetails { get; set; }
        public UserDetailsViewModel UserDetails { get; set; }
        public List<UserCarsViewModel> UserCars { get; set; }
        public RanksViewModel Ranks { get; set; }
        public List<SaledCarsViewModel> SaledCarsList { get; set; }
        public List<BuyCarViewModel> BuyCars { get; set; }
        public List<NotifyViewModel> Notifies { get; set; }
        public List<ServiceViewModel> Services { get; set; }
        public List<ServiceCarsViewModel> ServiceCars { get; set; }
    }
}

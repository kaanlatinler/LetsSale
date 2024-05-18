using LetsSale.Database;
using LetsSale.Models;
using LetsSale.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LetsSale.Controllers
{
    public class ServicesController : Controller
    {
        private readonly CarSaleDBContext _db;

        public ServicesController(CarSaleDBContext db)
        {
            _db = db;
        }

        public List<NotifyViewModel> GetNotifies()
        {
            var notifies = _db.Notify.ToList();

            var notifyVM = notifies.Where(x=>x.NotifyType == true).Select(x => new NotifyViewModel
            {
                CarID = x.CarID,
                UserID = x.UserID,
                NotifyTime = x.NotifyTime,
                NotifyID = x.NotifyID,
            }).ToList();

            return notifyVM;
        }

        public IActionResult Index()
        {
            var notifies = GetNotifies();
            var userCars = _db.UserCars.ToList();
            var categories = _db.CarCategories.ToList();
            var users = _db.Users.ToList();
            var employees = _db.Employee.ToList();
            var service = _db.ServiceCars.Where(x=>x.IsFinish == false).ToList();

            var CarDetailsVM = userCars.Select(x => new CarDetailsViewModel
            {
                BuyCars = notifies.Select(n => new BuyCarViewModel
                {
                    CarsList = new CarsViewModel
                    {
                        CMainID = n.CarID,
                        CarName = userCars.FirstOrDefault(bc => bc.CMainID == n.CarID)?.CarName,
                        CarBrand = userCars.FirstOrDefault(bc => bc.CMainID == n.CarID)?.CarBrand
                    },
                    UserDetailsList = new UserDetailsViewModel
                    {
                        UMainID = n.UserID,
                        UserName = users.FirstOrDefault(u => u.UMainID == n.UserID)?.UserName,
                        UserLastName = users.FirstOrDefault(u => u.UMainID == n.UserID)?.UserLastName
                    },
                    notify= new NotifyViewModel
                    {
                        NotifyID = n.NotifyID,
                        NotifyTime = n.NotifyTime,
                    }
                }).ToList(),
                Services = service.Select(s => new ServiceViewModel
                {
                    ServiceCars = new ServiceCarsViewModel
                    {
                        SstartDate = s.SstartDate,
                        SCarID = s.SCarID,
                        SEmployeeID = s.SEmployeeID,
                        SUserID = s.SUserID,
                        SMainID = s.SMainID,
                    },
                    UserDetails = new UserDetailsViewModel
                    {
                        UserName = users.FirstOrDefault(u=>u.UMainID == s.SUserID)?.UserName,
                        UserLastName = users.FirstOrDefault(u => u.UMainID == s.SUserID)?.UserLastName
                    },
                    UserCars = new UserCarsViewModel
                    {
                        CarName = userCars.FirstOrDefault(bc => bc.CMainID == s.SCarID)?.CarName,
                        CarBrand = userCars.FirstOrDefault(bc => bc.CMainID == s.SCarID)?.CarBrand,
                        CarYear = Convert.ToInt32(userCars.FirstOrDefault(bc => bc.CMainID == s.SCarID)?.CarYear),
                        CarPlateNumber = userCars.FirstOrDefault(bc => bc.CMainID == s.SCarID)?.CarPlateNumber,
                    }
                }).ToList(),
            }).ToList();
            return View(CarDetailsVM);
        }

        public IActionResult TakeCar(Guid CarID, Guid UserID)
        {
            var employeeMail = HttpContext.User.Identity.Name;

            var employee = _db.Employee.FirstOrDefault(x => x.EEmail == employeeMail);

            var serviceVM = new Service_Cars
            {
                IsFinish = false,
                SCarID = CarID,
                SEmployeeID = employee.EMainID,
                SMainID = Guid.NewGuid(),
                SstartDate = DateTime.Now,
                SUserID = UserID,
                SDesc = "",
                SfinishDate = DateTime.Now,
            };

            var notifyToDel = _db.Notify.FirstOrDefault(x => x.CarID == CarID);

            _db.ServiceCars.Add(serviceVM);
            _db.Notify.Remove(notifyToDel);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Index(ServiceCarsViewModel model)
        {
            var service = await _db.ServiceCars.FirstOrDefaultAsync(x=>x.SMainID == model.SMainID);

            service.IsFinish = true;
            service.SDesc = model.SDesc;
            service.SfinishDate = DateTime.Now;

            _db.ServiceCars.Update(service);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Services");
        }
    }
}

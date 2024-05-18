using LetsSale.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using LetsSale.Database;
using Microsoft.EntityFrameworkCore;
using LetsSale.Models.Entities;

namespace LetsSale.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly CarSaleDBContext _db;

		public HomeController(ILogger<HomeController> logger, CarSaleDBContext db)
		{
			_db = db;
			_logger = logger;
		}

		[AllowAnonymous]
		public IActionResult Index()
        {
			return View();
		}

        public IActionResult Main()
        {
            var cars = _db.Cars.ToList();
            var categories = _db.CarCategories.ToList();
            var pics = _db.CarPics.ToList();

            var CarDetailsVM = cars.Select(x => new CarDetailsViewModel
            {
                Cars = new CarsViewModel
                {
                    CarBrand = x.CarBrand,
                    CarName = x.CarName,
                    CarCategoryID = x.CarCategoryID,
                    CarMainPic = x.CarMainPic,
                    CarPlateNumber = x.CarPlateNumber,
                    CarPrice = x.CarPrice,
                    CarYear = x.CarYear,
                    CMainID = x.CMainID,
                    CarColor = x.CarColor,
                    CarPicsID = x.CarPicsID,
                    CarCargoVolume = x.CarCargoVolume,
                    CarHP = x.CarHP,
                    CarMaxFuelTankCapacity = x.CarMaxFuelTankCapacity,
                    CarMaxSpeed = x.CarMaxSpeed,
                    CarTorque = x.CarTorque,
                    CarTransmission = x.CarTransmission,
                    EmployeeID = x.EmployeeID
                },
                Categories = new CategoriesViewModel
                {
                    CatID = x.CarCategoryID,
                    CatName = categories.FirstOrDefault(c => c.CatID == x.CarCategoryID)?.CatName
                },
                Pics = pics.Where(y => y.CarID == x.CarPicsID).Select(p => new PicturesViewModel
                {
                    id = p.id,
                    CarPics = p.CarPicsPath,
                    CarID = p.CarID
                }).ToList()
            }).ToList();

            return View(CarDetailsVM);
        }

        public IActionResult BuyCar(Guid CarID)
        {
            var User = HttpContext.User.Identity.Name;
            var UserDetails = _db.Users.FirstOrDefault(x=>x.UserEmail == User);

            var notify = new Notifies
            {
                CarID = CarID,
                UserID = UserDetails.UMainID,
                NotifyTime = DateTime.Now,
                NotifyType = false
            };

            _db.Notify.Add(notify);
            _db.SaveChanges();

            return RedirectToAction("Main");
        }

        public IActionResult Service()
        {
            var userCars = _db.UserCars.ToList();
            var categories = _db.CarCategories.ToList();
            var userMail = HttpContext.User.Identity.Name;
            var user = _db.Users.FirstOrDefault(x => x.UserEmail == userMail);
            var employees = _db.Employee.ToList();
            var service = _db.ServiceCars.ToList();

            var CarDetailsVM = userCars.Select(x => new CarDetailsViewModel
            {

                UserDetails = new UserDetailsViewModel
                {
                    UMainID = user.UMainID,
                },
                Services = service.Select(s => new ServiceViewModel
                {
                    ServiceCars = new ServiceCarsViewModel
                    {
                        SstartDate = s.SstartDate,
                        SCarID = s.SCarID,
                        SEmployeeID = s.SEmployeeID,
                        SUserID = s.SUserID,
                        SMainID = s.SMainID,
                        SDesc = s.SDesc,
                        SfinishDate = s.SfinishDate,
                    },
                    EmployeeDetails = new EmployeeDetailsViewModel
                    {
                        EName = employees.FirstOrDefault(e => e.EMainID == s.SEmployeeID)?.EName,
                        ELastName = employees.FirstOrDefault(e => e.EMainID == s.SEmployeeID)?.ELastName,
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
            Console.WriteLine();
            return View(CarDetailsVM);
        }

        public IActionResult TakeService(Guid CarID)
        {
            var User = HttpContext.User.Identity.Name;
            var UserDetails = _db.Users.FirstOrDefault(x => x.UserEmail == User);

            var notify = new Notifies
            {
                CarID = CarID,
                UserID = UserDetails.UMainID,
                NotifyTime = DateTime.Now,
                NotifyType = true
            };

            _db.Notify.Add(notify);
            _db.SaveChanges();

            TempData["SuccessMsg"] = "İsteğiniz Başarıyla Bildirilmiştir. Lütfen Satıcıyla İletişime Geçiniz.";
            TempData["LoginTitle"] = "Bildirim Gönderildi!";

            return RedirectToAction("MyCars");
        }

        public IActionResult MyCars()
        {
            var UserMail = HttpContext.User.Identity.Name;
            var User = _db.Users.FirstOrDefault(y => y.UserEmail == UserMail);

            var cars = _db.UserCars.ToList();

            var UserCarsVM = cars.Where(c => c.UserID == User.UMainID).Select(x => new UserCarsViewModel { 
                UserID = User.UMainID,
                CMainID = x.CMainID,
                CarYear = x.CarYear,
                CarSaleDate = x.CarSaleDate,
                CarPlateNumber = x.CarPlateNumber,
                CarBrand = x.CarBrand,
                CarID = x.CarID,
                CarName = x.CarName
            });

            return View(UserCarsVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

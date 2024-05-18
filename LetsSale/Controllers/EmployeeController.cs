using LetsSale.Database;
using LetsSale.Models;
using LetsSale.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LetsSale.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly CarSaleDBContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(ILogger<EmployeeController> logger, CarSaleDBContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;

        }


        public List<NotifyViewModel> GetNotifies()
        {
            var notifies = _db.Notify.ToList();

            var notifyVM = notifies.Where(x=>x.NotifyType == false).Select(x => new NotifyViewModel
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
            var cars = _db.Cars.ToList();
            var categories = _db.CarCategories.ToList();
            var users = _db.Users.ToList();

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
                BuyCars = notifies.Select(n => new BuyCarViewModel
                {
                    CarsList = new CarsViewModel
                    {
                        CMainID = n.CarID,
                        CarName = cars.FirstOrDefault(bc => bc.CMainID == n.CarID)?.CarName,
                        CarBrand = cars.FirstOrDefault(bc => bc.CMainID == n.CarID)?.CarBrand
                    },
                    UserDetailsList = new UserDetailsViewModel
                    {
                        UMainID = n.UserID,
                        UserName = users.FirstOrDefault(u=>u.UMainID == n.UserID)?.UserName,
                        UserLastName = users.FirstOrDefault(u=>u.UMainID == n.UserID)?.UserLastName
                    },
                    notify = new NotifyViewModel
                    {
                        NotifyID = n.NotifyID,
                        NotifyTime = n.NotifyTime,
                    }
                    
                }).ToList(),
            }).ToList();

            return View(CarDetailsVM);
        }

        public IActionResult DeleteNotify(int NotifyID)
        {
            var notify = _db.Notify.FirstOrDefault(x=>x.NotifyID == NotifyID);

            _db.Notify.Remove(notify);
            _db.SaveChanges();

            return RedirectToAction("Index", "Employee");
        }

        public IActionResult AddCar()
        {
            var categories = _db.CarCategories.ToList();

            var cat = new AddCarViewModel();
            cat.CarCategories = new SelectList(_db.CarCategories
                .Select(c => new SelectListItem { Value = c.CatID.ToString(), Text = c.CatName })
                .ToList(), "Value", "Text");

            return View(cat);
        }

        public async Task<IActionResult> DeleteCar(Guid CarID)
        {
            try
            {
                var deletedCar = await _db.Cars.FirstOrDefaultAsync(x => x.CMainID == CarID);

                    var carPics = _db.CarPics.Where(x => x.CarID == CarID);
                    foreach (var pic in carPics)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, pic.CarPicsPath.TrimStart('/'));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        _db.CarPics.Remove(pic);
                    }

                    _db.Cars.Remove(deletedCar);
                    await _db.SaveChangesAsync();

                    TempData["SuccessMsg"] = "Araç başarıyla silindi!";
                    TempData["SuccessTitle"] = "Başarılı";
            }
            catch (Exception ex)
            {
                TempData["FailMsg"] = "Araç silinirken bir hata oluştu: " + ex.Message;
                TempData["FailTitle"] = "Hata";
            }

            return RedirectToAction("Index");
        }

        public IActionResult InspectCar(Guid CarID)
        {
            var cars = _db.Cars.ToList().Where(x=>x.CMainID == CarID);
            var categories = _db.CarCategories.ToList().Where(x=>x.CatID == cars.FirstOrDefault(c=>c.CMainID == CarID).CarCategoryID);
            var pics = _db.CarPics.ToList();

            var CarVM = cars.Select(x => new CarsViewModel
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
                CarCargoVolume = x.CarCargoVolume,
                CarHP = x.CarHP,
                CarMaxFuelTankCapacity = x.CarMaxFuelTankCapacity,
                CarMaxSpeed = x.CarMaxSpeed,
                CarTorque = x.CarTorque,
                CarTransmission = x.CarTransmission,
                EmployeeID = x.EmployeeID
            });

            var CatVM = categories.Select(x => new CategoriesViewModel
            {
                CatID = x.CatID,
                CatName = x.CatName,
            });

            var PicVM = pics.Select(x => new PicturesViewModel
            {
                CarPics = x.CarPicsPath,
                id = x.id,
                CarID = x.CarID,
            });

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
                Pics = pics.Where(y=>y.CarID == x.CarPicsID).Select(p=> new PicturesViewModel
                {
                   id = p.id,
                   CarPics = p.CarPicsPath,
                   CarID = CarID
                }).ToList()
            }).ToList();

            return View(CarDetailsVM);
        }

        public IActionResult SaledCars()
        {
            var saledCars = _db.SaledCars.ToList();
            var saledCarCategories = _db.CarCategories.ToList();
            var employees = _db.Employee.ToList();
            var users = _db.Users.ToList();
            var ranks = _db.ERanks.ToList();

            var SaledCarsVM = saledCars.Select(x => new CarDetailsViewModel
            {
                SaledCars = new SaledCarsViewModel
                {
                    EmployeeID = x.EmployeeID,
                    SCarBrand = x.SCarBrand,
                    SCarCategoryID = x.SCarCategoryID,
                    SCarMainPic = x.SCarMainPic,
                    SCarName = x.SCarName,
                    SCarPlateNumber = x.SCarPlateNumber,
                    SCarPrice = x.SCarPrice,
                    SCarSaleDate = x.SCarSaleDate,
                    UserId = x.UserId,
                    SCMainID = x.SCMainID,
                },
                Categories = new CategoriesViewModel
                {
                    CatID = x.SCarCategoryID,
                    CatName = saledCarCategories.FirstOrDefault(c => c.CatID == x.SCarCategoryID)?.CatName
                },
                EmployeeDetails = employees.Where(e => e.EMainID == x.EmployeeID).Select(em=> new EmployeeDetailsViewModel
                { 
                    EName = em.EName,
                    ELastName = em.ELastName
                }).FirstOrDefault(),
                UserDetails = users.Where(u => u.UMainID == x.UserId).Select(um=> new UserDetailsViewModel
                { 
                    UserName = um.UserName,
                    UserLastName = um.UserLastName
                }).FirstOrDefault(),
                Ranks = employees.Where(e => e.EMainID == x.EmployeeID).Select(r=> new RanksViewModel
                {
                    ERankID = r.ERankID,
                    ERankName = ranks.FirstOrDefault(c => c.ERankID == r.ERankID)?.ERankName
                }).FirstOrDefault()
            });

            return View(SaledCarsVM);
        }

        public IActionResult SaleCar(Guid CarID)
        {
            var carToSaled = _db.Cars.FirstOrDefault(x => x.CMainID == CarID);
            var categories = _db.CarCategories.FirstOrDefault(x => x.CatID == carToSaled.CarCategoryID);

            var SaleCarVM = new SaleCarViewModel
            {
                CarDetails = new CarDetailsViewModel
                {
                    Cars = new CarsViewModel
                    {
                        CarBrand = carToSaled.CarBrand,
                        CarName = carToSaled.CarName,
                        CarCategoryID = carToSaled.CarCategoryID,
                        CarMainPic = carToSaled.CarMainPic,
                        CarPlateNumber = carToSaled.CarPlateNumber,
                        CarPrice = carToSaled.CarPrice,
                        CarYear = carToSaled.CarYear,
                        CMainID = carToSaled.CMainID,
                        CarColor = carToSaled.CarColor,
                        CarCargoVolume = carToSaled.CarCargoVolume,
                        CarHP = carToSaled.CarHP,
                        CarMaxFuelTankCapacity = carToSaled.CarMaxFuelTankCapacity,
                        CarMaxSpeed = carToSaled.CarMaxSpeed,
                        CarTorque = carToSaled.CarTorque,
                        CarTransmission = carToSaled.CarTransmission,
                        EmployeeID = carToSaled.EmployeeID
                    },
                    Categories = new CategoriesViewModel
                    {
                        CatID = categories.CatID,
                        CatName = categories.CatName
                    },
                }
            };

            TempData["CarName"] = carToSaled.CarName;
            TempData["CarBrand"] = carToSaled.CarBrand;
            TempData["CMainID"] = carToSaled.CMainID;
            TempData["CarHP"] = carToSaled.CarHP;
            TempData["CarMaxSpeed"] = carToSaled.CarMaxSpeed;
            TempData["CarTorque"] = carToSaled.CarTorque;
            TempData["CarMaxFuelTankCapacity"] = carToSaled.CarMaxFuelTankCapacity;
            TempData["CarTransmission"] = carToSaled.CarTransmission;
            TempData["CarCargoVolume"] = carToSaled.CarCargoVolume;
            TempData["CarYear"] = carToSaled.CarYear;
            TempData["CarPlateNumber"] = carToSaled.CarPlateNumber;
            TempData["CarPrice"] = carToSaled.CarPrice;
            TempData["CarColor"] = carToSaled.CarColor;
            TempData["CatName"] = categories.CatName;
            TempData["CatID"] = categories.CatID;
            TempData["CarPicsID"] = carToSaled.CarPicsID;
            TempData["CarMainPic"] = carToSaled.CarMainPic;
            TempData["CarsID"] = carToSaled.CarsID;

            return View(SaleCarVM);
        }

        public IActionResult Users()
        {
            var users = _db.Users.ToList();
            var userCars = _db.UserCars.ToList();

            var userDetails = users.Select(x => new CarDetailsViewModel
            {
                UserDetails = new UserDetailsViewModel
                {
                    UMainID = x.UMainID,
                    UserCarsID = x.UserCarsID,
                    UserEmail = x.UserEmail,
                    UserLastName = x.UserLastName,
                    UserName = x.UserName,
                    UserPassword = x.UserPassword,
                    UserPhoneNumber = x.UserPhoneNumber,
                    UserRegisterDate = x.UserRegisterDate
                },
                UserCars = userCars.Where(car => car.UserID == x.UserCarsID)
                .Select(car => new UserCarsViewModel
                {
                    UserID = car.UserID,
                    CarBrand = car.CarBrand,
                    CMainID = car.CMainID,
                    CarYear = car.CarYear,
                    CarSaleDate = car.CarSaleDate,
                    CarPlateNumber = car.CarPlateNumber,
                    CarName = car.CarName
                }).ToList()
            }).ToList();

            return View(userDetails);
        }

        public IActionResult Employees()
        {
            var employees = _db.Employee.ToList();
            var ranks = _db.ERanks.ToList();
            var saledCars = _db.SaledCars.ToList();

            var employeeDetails = employees.Select(x => new CarDetailsViewModel
            {
                EmployeeDetails = new EmployeeDetailsViewModel
                {
                    EMainID = x.EMainID,
                    EEmail = x.EEmail,
                    ELastName = x.ELastName,
                    EName = x.EName,
                    EPassword = x.EPassword,
                    EPhoneNumber = x.EPhoneNumber,
                    ERankID = x.ERankID,
                    EStartDate = x.EStartDate,
                    SaledCarsID = x.SaledCarsID
                },
                Ranks = new RanksViewModel
                {
                    ERankID = x.ERankID,
                    ERankName = ranks.FirstOrDefault(r => r.ERankID == x.ERankID)?.ERankName
                },
                SaledCarsList = saledCars.Where(sc => sc.EmployeeID == x.EMainID)
                .Select(car => new SaledCarsViewModel
                {
                    EmployeeID = car.EmployeeID,
                }).ToList()
            }).ToList();

            return View(employeeDetails);
        }

        //POST

        [HttpPost]
        public async Task<IActionResult> SaleCar(SaleCarViewModel model)
        {
            var employeeEmail = HttpContext.User.Identity.Name;

            var EmployeeDetails = await _db.Employee.FirstOrDefaultAsync(x => x.EEmail == employeeEmail);

            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserEmail == model.UserDetails.UserEmail);

            var carDetails = await _db.Cars.FirstOrDefaultAsync(x=>x.CMainID == model.CarDetails.Cars.CMainID);

            if (EmployeeDetails != null && user != null)
            {
                var saledCar = new Saled_Cars
                {
                    SCarCategoryID = model.CarDetails.Categories.CatID,
                    EmployeeID = EmployeeDetails.EMainID,
                    SCMainID = model.CarDetails.Cars.CMainID,
                    SCarBrand = model.CarDetails.Cars.CarBrand,
                    SCarName = model.CarDetails.Cars.CarName,
                    SCarMainPic = model.CarDetails.Cars.CarMainPic,
                    SCarPlateNumber = model.CarDetails.Cars.CarPlateNumber,
                    SCarPrice = model.CarDetails.Cars.CarPrice.ToString(),
                    SCarSaleDate = DateTime.Now,
                    UserId = user.UMainID
                };

                var userCar = new User_Cars
                {
                    UserID = user.UMainID,  
                    CMainID = model.CarDetails.Cars.CMainID,
                    CarBrand = model.CarDetails.Cars.CarBrand,
                    CarName = model.CarDetails.Cars.CarName,
                    CarPlateNumber = model.CarDetails.Cars.CarPlateNumber,
                    CarSaleDate = DateTime.Now,
                    CarYear = model.CarDetails.Cars.CarYear,
                };

                user.UserCarsID = user.UMainID;

                _db.SaledCars.Add(saledCar);
                _db.UserCars.Add(userCar);
                _db.Users.Update(user);
                _db.Cars.Remove(carDetails);
                await _db.SaveChangesAsync();

                TempData["SuccessMsg"] = "Araç başarıyla Satıldı!";
                TempData["SuccessTitle"] = "Başarılı";

                return RedirectToAction("SaledCars");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(AddCarViewModel model, List<IFormFile> carPics)
        {
            Guid carID = Guid.NewGuid();

            var employeeEmail = HttpContext.User.Identity.Name;

            var EmployeeDetails = _db.Employee.FirstOrDefaultAsync(x => x.EEmail == employeeEmail).Result;

            var newCar = new Car
                {
                    CarBrand = model.CarBrand,
                    CarName = model.CarName,
                    CarPrice = model.CarPrice,
                    CarYear = model.CarYear,
                    CarColor = model.CarColor,
                    CarCategoryID = model.CarCategoryID,
                    CarPlateNumber = model.CarPlateNumber,
                    CMainID = carID,
                    CarCargoVolume = model.CarCargoVolume,
                    CarHP = model.CarHP,
                    CarMaxFuelTankCapacity = model.CarMaxFuelTankCapacity,
                    CarMaxSpeed = model.CarMaxSpeed,
                    CarTorque = model.CarTorque,
                    CarTransmission = model.CarTransmission,
                    EmployeeID = EmployeeDetails.EMainID
                };

                if (carPics != null && carPics.Count > 0)
                {
                    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "CarPics");

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    string FilePath= " ";

                    foreach (var pic in carPics)
                    {
                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(pic.FileName)}";
                        string filePath = Path.Combine(uploadPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await pic.CopyToAsync(stream);
                        }

                        FilePath = $"/CarPics/{fileName}";
                        var newPic = new Car_Pics
                        {
                            CarPicsPath = FilePath,
                            CarID = carID
                        };
                        
                        _db.CarPics.Add(newPic);
                        await _db.SaveChangesAsync();
                    }

                    var carPic = _db.CarPics.FirstOrDefault(x=>x.CarPicsPath == FilePath);
                    newCar.CarMainPic = carPic.CarPicsPath;
                    newCar.CarPicsID = carPic.CarID;
                }

                var oldCar = await _db.Cars.FirstOrDefaultAsync(x => x.CarPlateNumber == model.CarPlateNumber);

                if (oldCar != null)
                {
                    TempData["FailMsg"] = "Aynı Plakaya Sahip Başka Bir Araç Mevcut!";
                    TempData["FailTitle"] = "Kayıtlı Plaka";
                }
                else
                {
                    await _db.Cars.AddAsync(newCar);
                    await _db.SaveChangesAsync();

                    TempData["SuccessMsg"] = "Araç Başarıyla Eklendi!";
                    TempData["SuccessTitle"] = "Kayıt Başarılı";
                }


            return RedirectToAction("AddCar");
        }
    }
}

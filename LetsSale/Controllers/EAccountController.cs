using LetsSale.Database;
using LetsSale.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using LetsSale.Models.Entities;

namespace LetsSale.Controllers
{
    public class EAccountController : Controller
    {
        private readonly CarSaleDBContext dBContext;

        public EAccountController(CarSaleDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var employeeMail = await dBContext.Employee.FirstOrDefaultAsync(x=>x.EEmail == viewModel.UserEmail);

            if (employeeMail != null)
            {
                var employeePass = await dBContext.Employee.FirstOrDefaultAsync(x => x.EPassword == viewModel.UserPassword);

                if (employeePass != null)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, viewModel.UserEmail),
                        new Claim(ClaimTypes.Role, "Employee"),
                        new Claim(ClaimTypes.Name, employeeMail.EEmail)
                    };

                    ClaimsIdentity CI = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties prop = new AuthenticationProperties()
                    {
                        AllowRefresh = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(CI), prop);

                    var EmployeeVM = new EmployeeDetailsViewModel
                    {
                        EEmail = employeeMail.EEmail,
                        ELastName = employeeMail.ELastName,
                        EName = employeeMail.EName,
                        EPassword = employeeMail.EPassword,
                        EPhoneNumber = employeeMail.EPhoneNumber,
                        EMainID = employeeMail.EMainID,
                        ERankID = employeeMail.ERankID,
                        EStartDate = employeeMail.EStartDate,
                        SaledCarsID = employeeMail.SaledCarsID
                    };

                    TempData["SuccessMsg"] = "Başarıyla Giriş Yaptınız! \nYönlendiriliyosunuz...";
                    TempData["LoginTitle"] = "Giriş Başarılı";
                    TempData["RedirectUrl"] = "Employee/Index";

                }
                else
                {
                    TempData["FailMsg"] = "Hatalı Şifre Girdiniz. Lütfen Tekrar Deneyiniz!";
                    TempData["LoginTitle"] = "Üzgünüm Şuanda İsteğini Gerçekleştiremiyorum";
                }
            }
            else
            {
                TempData["FailMsg"] = "Böyle Bir Eposta Sistemimize Kayıtlı Değil!";
                TempData["LoginTitle"] = "Kayıtsız Kullanıcı";
            }

            return RedirectToAction("Login");
        }

        public IActionResult AddEmployee()
        {
            var ranks = dBContext.ERanks.ToList();

            var rank = new AddEmployeeViewModel();
            rank.Ranks = new SelectList(dBContext.ERanks
                .Select(r=> new SelectListItem { Value = r.ERankID.ToString(), Text = r.ERankName})
                .ToList(), "Value", "Text");

            return View(rank);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeViewModel model)
        {
            Guid id = Guid.NewGuid();

            var user = new Employees
            {
                EMainID = id,
                EEmail = model.EEmail,
                ELastName = model.ELastName,
                EName = model.EName,
                EPassword = model.EPassword,
                EPhoneNumber = model.EPhoneNumber,
                ERankID = model.ERankID,
                EStartDate = DateTime.Now,
                SaledCarsID = 0
            };

            var oldUser = await dBContext.Employee.FirstOrDefaultAsync(x => x.EEmail == model.EEmail);
            if (oldUser != null)
            {
                TempData["FailMsg"] = "Aynı Email İle Birden Fazla Hesap Açamazsınız!";
                TempData["SignUpTitle"] = "Üzgünüm Şuanda İsteğini Gerçekleştiremiyorum";
            }
            else
            {
                await dBContext.Employee.AddAsync(user);
                await dBContext.SaveChangesAsync();

                TempData["SuccessMsg"] = "Kayıt İşlemi Başarılı!";
                TempData["SignUpTitle"] = "Kayıt Başarılı";
            }

            return RedirectToAction("AddEmployee");
        }
    }
}

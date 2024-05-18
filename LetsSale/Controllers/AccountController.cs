using LetsSale.Database;
using LetsSale.Models;
using LetsSale.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace LetsSale.Controllers
{
    public class AccountController : Controller
	{
        private readonly CarSaleDBContext dBContext;

        public AccountController(CarSaleDBContext dBContext) 
		{
            this.dBContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
		public IActionResult Login()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var userMail = await dBContext.Users.FirstOrDefaultAsync(x=> x.UserEmail == viewModel.UserEmail);
            var employeeMail = await dBContext.Employee.FirstOrDefaultAsync(x=> x.EEmail == viewModel.UserEmail);

            if (userMail != null)
            {
                var userPass = await dBContext.Users.FirstOrDefaultAsync(x => x.UserPassword == viewModel.UserPassword);

                if(userPass != null)
                {
                    List<Claim> claims = new List<Claim>() {
                        new Claim(ClaimTypes.NameIdentifier, viewModel.UserEmail),
                        new Claim(ClaimTypes.Role, "User"),
                        new Claim(ClaimTypes.Name, viewModel.UserEmail)

                    };

                    ClaimsIdentity CI = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties prop = new AuthenticationProperties()
                    {
                        AllowRefresh = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(CI), prop);

                    TempData["SuccessMsg"] = "Başarıyla Giriş Yaptınız! \nYönlendiriliyosunuz...";
                    TempData["LoginTitle"] = "Giriş Başarılı";
                    TempData["user"] = "user";
                } 
                else
                {
                    TempData["FailMsg"] = "Hatalı Şifre Girdiniz. Lütfen Tekrar Deneyiniz!";
                    TempData["LoginTitle"] = "Üzgünüm Şuanda İsteğini Gerçekleştiremiyorum";
                }
            }
            else if (employeeMail != null)
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

                    TempData["SuccessMsg"] = "Başarıyla Giriş Yaptınız! \nYönlendiriliyosunuz...";
                    TempData["LoginTitle"] = "Giriş Başarılı";
                    TempData["user"] = "employee";

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
		
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Register(SignUpViewModel viewModel)
        {
            Guid id = Guid.NewGuid();

            var user = new User
            {
                UMainID = id,
                UserName = viewModel.UserName,
                UserLastName = viewModel.UserLastName,
                UserEmail = viewModel.UserEmail,
                UserPassword = viewModel.UserPassword,
                UserPhoneNumber = viewModel.UserPhoneNumber,
                UserRegisterDate = DateTime.Now,
            };

            var oldUser = await dBContext.Users.FirstOrDefaultAsync(x => x.UserEmail == viewModel.UserEmail);
            if (oldUser != null)
            {
                TempData["FailMsg"] = "Aynı Email İle Birden Fazla Hesap Açamazsınız!";
                TempData["SignUpTitle"] = "Üzgünüm Şuanda İsteğini Gerçekleştiremiyorum";
            }
            else
            {
                await dBContext.Users.AddAsync(user);
                await dBContext.SaveChangesAsync();

                TempData["SuccessMsg"] = "Başarıyla Kayıt Oldunuz Lütfen Giriş Yapın!";
                TempData["SignUpTitle"] = "Kayıt Başarılı";
            }

            return RedirectToAction("Register");
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel viewModel)
        {
            Guid id = Guid.NewGuid();

            var user = new User
            {
                UMainID = id,
                UserName = viewModel.UserName,
                UserLastName = viewModel.UserLastName,
                UserEmail = viewModel.UserEmail,
                UserPassword = viewModel.UserPassword,
                UserPhoneNumber = viewModel.UserPhoneNumber,
                UserRegisterDate = DateTime.Now,
            };

            var oldUser = await dBContext.Users.FirstOrDefaultAsync(x => x.UserEmail == viewModel.UserEmail);
            if (oldUser != null)
            {
                TempData["FailMsg"] = "Aynı Email İle Birden Fazla Hesap Açamazsınız!";
                TempData["SignUpTitle"] = "Üzgünüm Şuanda İsteğini Gerçekleştiremiyorum";
            }
            else
            {
                await dBContext.Users.AddAsync(user);
                await dBContext.SaveChangesAsync();

                TempData["SuccessMsg"] = "Kayıt İşlemi Başarılı!";
                TempData["SignUpTitle"] = "Kayıt Başarılı";
            }

            return RedirectToAction("AddUser");
        }
    }
}

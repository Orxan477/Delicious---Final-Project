using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Restaurant.Business.Services;
using Restaurant.Business.Utilities;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Account;
using Restaurant.Business.ViewModels.Menu;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private AppDbContext _context;
        private IConfiguration _configure;
        private IWebHostEnvironment _env;
        private SettingServices _settingServices;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(AppDbContext context,
                                 UserManager<AppUser> userManager,
                                 IConfiguration configure,
                                 IWebHostEnvironment env,
                                 RoleManager<IdentityRole> roleManager,
                                 SettingServices settingServices,
                                 SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _configure = configure;
            _env = env;
            _roleManager = roleManager;
            _settingServices = settingServices;
            _signInManager = signInManager;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public IActionResult Register()
        {

            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!IsAuthenticated())
            {
                return BadRequest();
            }
            if (!ModelState.IsValid) return View();
            AppUser newUser = new AppUser
            {
                FullName = registerVM.FullName,
                Email = registerVM.Email,
                UserName = registerVM.UserName,
                PhoneNumber = registerVM.Number
            };
            IdentityResult identityResult = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (identityResult.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                string link = Url.Action(nameof(VerifyEmail), "Account", new { userId = newUser.Id, token },
                                                                    Request.Scheme, Request.Host.ToString());
                string name = GetSetting("RestaurantName");
                string body = string.Empty;
                using (StreamReader streamReader = new StreamReader(Path.Combine(_env.WebRootPath, "assets", "SendMessage", "ConfigurationLink.html")))
                {
                    body = streamReader.ReadToEnd();
                }
                body = body.Replace("{{email}}", $"{newUser.Email}").Replace("{{url}}", $"{link}").Replace("{{restaurantName}}", $"{name}");
                Email.SendEmail(_configure.GetSection("Email:SenderEmail").Value,
                           _configure.GetSection("Email:Password").Value, newUser.Email, body, $"{name} - Confirmation Link");
                //await _userManager.AddToRoleAsync(newUser, "Admin");
                await _userManager.AddToRoleAsync(newUser, "Member");
                ViewBag.IsSuccessful = true;
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
        }
        public async Task<IActionResult> VerifyEmail(string userid, string token)
        {
            bool isExistToken = _context.TokenBlackList.Any(x => x.Token == token);
            if (isExistToken) return BadRequest();
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null) return NotFound();
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                user.IsActivated = true;
                await _context.SaveChangesAsync();
                string name = GetSetting("RestaurantName");
                string body = string.Empty;
                using (StreamReader streamReader = new StreamReader(Path.Combine(_env.WebRootPath, "assets", "SendMessage", "ConfigurationLinkSuccesfull.html")))
                {
                    body = streamReader.ReadToEnd();
                }
                body = body.Replace("{{restaurantName}}", $"{name}");
                Email.SendEmail(_configure.GetSection("Email:SenderEmail").Value,
                           _configure.GetSection("Email:Password").Value, user.Email, body, $"{name} - Confirmation Succesfull");
                await _signInManager.SignInAsync(user, false);

                await AddTokenDb(token);

                await CookieAddDb(user);
                return RedirectToAction("Index", "Home");
            }
            else return BadRequest();
        }
        private bool IsAuthenticated()
        {
            if (User.Identity.IsAuthenticated)
            {
                return false;
            }
            return true;
        }
        private async Task AddTokenDb(string token)
        {
            TokenBlackList blackList = new TokenBlackList
            {
                Token = token
            };
            _context.TokenBlackList.Add(blackList);
            await _context.SaveChangesAsync();
        }
        public IActionResult Login()
        {
            if (!IsAuthenticated())
            {
                return BadRequest();
            }
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM, string ReturnUrl)
        {
            IsAuthenticated();
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }

            AppUser user = await _userManager.FindByNameAsync(loginVM.UserName);
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Username and Password is Wrong");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(loginVM);
            }
            if (user.IsDeleted == false)
            {
                ModelState.AddModelError(string.Empty, "Your account is blocked");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(loginVM);
            }
            if (user.IsActivated == false)
            {
                ModelState.AddModelError(string.Empty, "Please Confirm Your Email");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(loginVM);
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Your Account is locked. Few minutes leter is unlocked");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(loginVM);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username and Password is Wrong");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(loginVM);

            }
            await CookieAddDb(user);

            if (ReturnUrl != null)
            {
                return LocalRedirect(ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        private async Task CookieAddDb(AppUser user)
        {
            List<BasketVM> basket = GetBasket();
            if (basket != null)
            {
                foreach (var item in basket)
                {
                    var type = _context.Types.Where(x => x.Name == item.Size).FirstOrDefault();
                    BasketItem newDbBasket = new BasketItem
                    {
                        ProductId = item.Id,
                        Count = item.Count,
                        Price = item.Price,
                        TypeId = type.Id,
                        AppUserId = user.Id
                    };
                    await _context.BasketItems.AddAsync(newDbBasket);
                    await _context.SaveChangesAsync();
                    Response.Cookies.Delete("basket");
                }
            }
        }
        private List<BasketVM> GetBasket()
        {
            List<BasketVM> basket;
            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }
            return basket;
        }
        public IActionResult LogOut(string ReturnUrl)
        {
            _signInManager.SignOutAsync();
            if (ReturnUrl != null)
            {
                return LocalRedirect(ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
                }
            }
            return Content("Ok");
        }
        public IActionResult SettingAccount()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SettingAccount(string ReturnUrl)
        {

            if (ReturnUrl != null)
            {
                return LocalRedirect(ReturnUrl);
            }
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        public IActionResult ChangePassword()
        {
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            if (!ModelState.IsValid) return View(changePasswordVM);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User is Not Found");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            var result = await _userManager.ChangePasswordAsync(user, changePasswordVM.CurrentPassword,
                                                                                    changePasswordVM.NewPassword);
            if (result.Succeeded)
            {
                ViewBag.IsSuccessPassword = true;
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(nameof(SettingAccount));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(changePasswordVM);
            }
        }

        public IActionResult ChangeUsername()
        {
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUsername(ChangeUsernameVM changeUsername)
        {
            if (!ModelState.IsValid) return View(changeUsername);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User is Not Found");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(changeUsername);
            }
            var checkPasword = await _userManager.CheckPasswordAsync(user, changeUsername.Password);
            if (!checkPasword)
            {
                ModelState.AddModelError(string.Empty, "Incorrect Password");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(changeUsername);
            }
            var result = await _userManager.SetUserNameAsync(user, changeUsername.NewUsername);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                ViewBag.IsSuccessUsername = true;
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(nameof(SettingAccount));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
        }
        public IActionResult ChangeMail()
        {
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeMail(ChangeEmailVM changeEmail)
        {
            if (!ModelState.IsValid) return View(changeEmail);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User is Not Found");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(changeEmail);
            }

            var checkPasword = await _userManager.CheckPasswordAsync(user, changeEmail.Password);
            if (!checkPasword)
            {
                ModelState.AddModelError(string.Empty, "Incorrect Password");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(changeEmail);
            }
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, changeEmail.NewEmail);
            var result = await _userManager.ChangeEmailAsync(user, changeEmail.NewEmail, token);
            if (result.Succeeded)
            {
                ViewBag.IsSuccessMail = true;
                await AddTokenDb(token);
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(nameof(SettingAccount));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(nameof(ChangeMail));
            }
        }
        //public async Task<IActionResult> VerifyChangeEmail(AppUser user,string email,string token)
        //{
        //    var result = await _userManager.ChangeEmailAsync(user, email, token);
        //    if (result.Succeeded)
        //    {
        //        //Email.SendEmailAsync(user.Email, "Your Email Is Changed", "Fiorello");
        //        ViewBag.IsSuccessMail = true;
        //        return View(nameof(SettingAccount));
        //    }
        //    else
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //        return View(nameof(ChangeMail));
        //    }
        //}
        public IActionResult ForgotPassword()
        {
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(MailVm mail)
        {
            if (!ModelState.IsValid) return View(mail);
            AppUser user = await _userManager.FindByEmailAsync(mail.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User Is Not Found");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            if (user.IsActivated == false)
            {
                ModelState.AddModelError(string.Empty, "Please Confirm Your Email");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(mail);
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string link = Url.Action(nameof(ForgotPasswordConfirm), "Account", new { userId = user.Id, token },
                                                                            Request.Scheme, Request.Host.ToString());
           
            string name = GetSetting("RestaurantName");
            string body = string.Empty;
            using (StreamReader streamReader = new StreamReader(Path.Combine(_env.WebRootPath, "assets", "SendMessage", "ResetPassword.html")))
            {
                body = streamReader.ReadToEnd();
            }
            body = body.Replace("{{restaurantName}}", $"{name}").Replace("{{url}}",link);
            Email.SendEmail(_configure.GetSection("Email:SenderEmail").Value,
                       _configure.GetSection("Email:Password").Value, user.Email, body, $"{name} - Reset Password");
            await _signInManager.SignInAsync(user, false);
            ViewBag.IsSuccessReset = true;
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        public IActionResult ForgotPasswordConfirm()
        {
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPasswordConfirm(string userid, string token,ForgotPasswordVM forgotPassword)
        {
            bool isExistToken = _context.TokenBlackList.Any(x => x.Token == token);
            if (isExistToken) return BadRequest();
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByIdAsync(userid);
            if (user == null) return BadRequest();
            IdentityResult identityResult = await _userManager.ResetPasswordAsync(user, token, forgotPassword.NewPassword);
            if (identityResult.Succeeded)
            {
                await AddTokenDb(token);
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
        }
    }
}
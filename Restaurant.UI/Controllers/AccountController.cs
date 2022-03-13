﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Restaurant.Business.Services;
using Restaurant.Business.Utilities;
using Restaurant.Business.ViewModels.Account;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.IO;
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
                string name= GetSetting("RestaurantName");
                string body = string.Empty;
                using (StreamReader streamReader = new StreamReader(Path.Combine(_env.WebRootPath, "assets", "SendMessage", "ConfigurationLink.html")))
                {
                    body = streamReader.ReadToEnd();
                }
                body = body.Replace("{{email}}", $"{newUser.Email}").Replace("{{url}}", $"{link}").Replace("{{restaurantName}}",$"{name}");
                Email.SendEmail(_configure.GetSection("Email:SenderEmail").Value,
                           _configure.GetSection("Email:Password").Value, newUser.Email, body, $"{name} - Confirmation Link");
                await _userManager.AddToRoleAsync(newUser, "Admin");
                //await _userManager.AddToRoleAsync(newUser, "Member");
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
            var user=await _userManager.FindByIdAsync(userid);
            if (user == null) return NotFound();
            var result=await _userManager.ConfirmEmailAsync(user, token);
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
                await _signInManager.SignInAsync(user,false);
                return RedirectToAction("Index","Home");
            }
            else return BadRequest();
        }
        public IActionResult Login()
        {
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM,string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
                
            AppUser user=await _userManager.FindByNameAsync(loginVM.UserName);
            if(user is null)
            {
                ModelState.AddModelError(string.Empty, "Email and Password is Wrong");
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
                ModelState.AddModelError(string.Empty, "Email and Password is Wrong");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(loginVM);
            }

            if (ReturnUrl != null)
            {
                return LocalRedirect(ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
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
    }
}
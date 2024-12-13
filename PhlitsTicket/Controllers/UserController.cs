using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using Models.ViewModels;
using System.IO;
using Utility;

namespace PhlitsTicket.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManger;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Register()
        {
            if (_roleManager.Roles.IsNullOrEmpty())
            {
                await _roleManager.CreateAsync(new(StaticData.Admin));
                await _roleManager.CreateAsync(new(StaticData.User));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterVM user)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser newUser = new()
                {
                    UserName = user.UserName,
                    Email = user.Email
                };
                if (user.ImgUrl != null)
                {
                    var imgName = newUser.UserName.ToString() + Path.GetExtension(user.ImgUrl.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "users", imgName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        user.ImgUrl.CopyTo(stream);
                    }
                    newUser.ImgUrl = imgName;
                }
                var result = await _userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, StaticData.Admin);
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVM userVM)
        {
            var applicationUser = await _userManager.FindByNameAsync(userVM.UserName);
            if (applicationUser != null && await _userManager.CheckPasswordAsync(applicationUser, userVM.Password))
            {
                await _signInManager.SignInAsync(applicationUser, userVM.RememberMe);
                TempData["logedIn"] = "Loged in successfully";
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Password", "Invalid Username Or Password");
            return View(userVM);
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }
        [Authorize(Roles = $"{StaticData.Admin}")]
        public IActionResult CreateNewAdmin()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{StaticData.Admin}")]
        public async Task<IActionResult> CreateNewAdmin(UserRegisterVM userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    UserName = userVM.UserName,
                    Email = userVM.Email,
                };
                if (userVM.ImgUrl != null)
                {
                    var imgName = userVM.UserName.ToString() + Path.GetExtension(userVM.ImgUrl.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "users", imgName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        userVM.ImgUrl.CopyTo(stream);
                    }
                    user.ImgUrl = imgName;
                }
                var result = await _userManager.CreateAsync(user, userVM.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, StaticData.Admin);
                    TempData["CreatedAdmin"] = "User Created";
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Password", "Password It must consist of at least 6 letters, lowercase and uppercase");
            }
            return View(userVM);
        }
    }
}

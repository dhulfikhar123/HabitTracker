using AspNetCoreHero.ToastNotification.Abstractions;
using HabitTracker.Models;
using HabitTracker.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Controllers
{
    public class AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, /*INotyfService notyf,*/ IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        //private readonly INotyfService _notyfService = notyf;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt");
                //_notyfService.Warning("Invalid Login attempt!");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                    //_notyfService.Warning("Invalid Login attempt!");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);

                if (_userManager == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                    //_notyfService.Warning("Invalid Login attempt!");
                    return View(model);
                }

                if (result.Succeeded)
                {
                    var userDetails = await Helper.GetCurrentUserIdAsync(_httpContextAccessor, _userManager);
                    //_notyfService.Success("Login successful");
                    return RedirectToAction("Index", "Habit");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    //_notyfService.Warning("Invalid Login attempt!");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == model.Username || u.Email == model.Email);

                if (existingUser != null)
                {
                    /*_notyfService.Error("Owned by Another User!");*/
                    return View();
                }

                var user = new IdentityUser
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //_notyfService.Success("Account Created Successfully!");
                    return RedirectToAction("Login");
                }
                //_notyfService.Error("An error occurred while registering");
                return View();
            }
            return View();
        }
    }
}

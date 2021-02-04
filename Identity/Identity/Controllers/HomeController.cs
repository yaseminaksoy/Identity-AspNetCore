using Identity.Context;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [AutoValidateAntiforgeryToken] //for security on every post methods in controller
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View(new UserSignInViewModel());
        }
        [HttpPost]
        //[ValidateAntiForgeryToken] //for security only for this method
        public async Task<IActionResult> SignIn(UserSignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
                if (identityResult.IsLockedOut)
                {
                    var result = await _userManager.GetLockoutEndDateAsync(await _userManager.FindByNameAsync(model.UserName));
                    var limitedTime = result.Value;
                    var remainingMinutes = limitedTime.Minute - DateTime.Now.Minute;

                    ModelState.AddModelError("", $"Since you entered incorrect password for 3 times, your account has been locked out for {remainingMinutes} min");
                    
                    return View("Index", model);
                }

                if (identityResult.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Please confirm your email");
                    return View("Index", model);
                }

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }
                var incorrectEnterCount = await _userManager.GetAccessFailedCountAsync( await _userManager.FindByNameAsync(model.UserName));
                ModelState.AddModelError("", $"Incorrect Username or Password. You have {3-incorrectEnterCount} more chance");
            }
            return View("Index",model);
        }
        public IActionResult SignUp()
        {
            return View(new UserSignUpViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.UserName,
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email
                };
               var result = await _userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}

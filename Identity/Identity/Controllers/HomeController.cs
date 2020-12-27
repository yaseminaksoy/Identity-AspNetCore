using Identity.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new UserSignInViewModel());
        }
        public IActionResult SignUp()
        {
            return View(new UserSignUpViewModel());
        }
    }
}

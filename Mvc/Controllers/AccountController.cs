using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignInUser(string name)
        {
            
            Program.CurrentUser = name;
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RegisterUser(string name)
        {

            Program.CurrentUser = name;
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
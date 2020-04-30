using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Controllers
{
    public class AccountController : Controller
    {
        private UserService _userService;

        public AccountController()
        {
            _userService = new UserService();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignInUser(string name)
        {
            var user = _userService.Find(user => user.UserName == name);
            if(user.Count > 0)
            {
                Program.CurrentUser = user[0].Id;
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RegisterUser(string name)
        {
            var user = _userService.Create(new User() {UserName = name});
            Program.CurrentUser = user.Id;
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
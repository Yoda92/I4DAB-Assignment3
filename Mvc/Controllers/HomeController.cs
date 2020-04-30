using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Data.Entities;
using Data.Services;

namespace Mvc.Controllers {
    public class HomeController : Controller {
        private UserService _userService;

        public HomeController()
        {
            _userService = new UserService();
        }

        public IActionResult Index ()
        {
            var user = _userService.GetById(Program.CurrentUser);

            return View (user);
        }

        public IActionResult Privacy () {
            return View ();
        }
    }
}
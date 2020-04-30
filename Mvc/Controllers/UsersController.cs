using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mvc.Controllers {
    public class UsersController : Controller {

        UserService _userService;
        public UsersController () {
            _userService = new UserService ();
        }

        public IActionResult Index () {
            var users = _userService.GetAll ();
            var currentUser = _userService.GetById (Program.CurrentUser);
            ViewBag.users = users;
            ViewBag.currentUser = currentUser;
            return View (users);
        }

        [Route ("Users/Follow/{userId}")]
        public IActionResult Follow (string userId) {
            var follower = _userService.GetById (Program.CurrentUser);
            var toFollow = _userService.GetById (userId);
            _userService.FollowUser (toFollow, follower);
            return RedirectToAction ("Index");
        }

        [Route ("Users/Blacklist/{userId}")]
        public IActionResult Blacklist (string userId) {
            var blacklister = _userService.GetById (Program.CurrentUser);
            var toBlacklist = _userService.GetById (userId);
            _userService.BlacklistUser (toBlacklist, blacklister);
            return RedirectToAction ("Index");
        }
    }
}
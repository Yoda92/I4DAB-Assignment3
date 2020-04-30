using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Repositories;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mvc.Controllers {
    public class UsersController : Controller {

        public class WallDto {
            public List<Post> Posts { get; set; }
            public User WallUser { get; set; }
        }

        UserService _userService;
        WallRepository _wallrepo;
        public UsersController () {
            _userService = new UserService ();
            _wallrepo = new WallRepository ();
        }

        public IActionResult Index () {
            var users = _userService.GetAll ();
            var currentUser = _userService.GetById (Program.CurrentUser);
            ViewBag.users = users;
            ViewBag.currentUser = currentUser;
            return View (users);
        }

        [Route ("Users/Wall/{userId}")]
        public IActionResult Wall (string userId) {
            WallDto dto = new WallDto () {
                Posts = _wallrepo.GetPosts (userId, Program.CurrentUser), //wallService.getsomething
                WallUser = _userService.GetById (userId)
            };

            return View (dto);
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
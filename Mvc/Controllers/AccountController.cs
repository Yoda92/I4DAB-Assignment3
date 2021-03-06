﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Controllers {
    public class AccountController : Controller {
        private UserService _userService;

        public AccountController () {
            _userService = new UserService ();
        }
        public IActionResult Index () {
            return View ();
        }

        public IActionResult SignInUser (string name) {
            var user = _userService.Find (name);
            if (user.Count > 0) {
                Program.CurrentUser = user[0].Id;
                Program.CurrentUserName = user[0].UserName;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction ("SignIn", "Account");
        }

        public IActionResult RegisterUser (string name, string gender, int age) {
            var user = _userService.Create (new User () {
                UserName = name,
                    Age = age,
                    BlackListUserIds = new List<string> (),
                    FollowUserIds = new List<string> (),
                    Gender = gender,
                    PostIds = new List<string> (),
                    SubscribedCircleIds = new List<string> ()
            });
            Program.CurrentUser = user.Id;
            Program.CurrentUserName = user.UserName;
            return RedirectToAction ("Index", "Home");
        }

        public IActionResult SignOut () {
            Program.CurrentUser = null;
            Program.CurrentUserName = null;
            return RedirectToAction ("Index", "Home");
        }

        public IActionResult SignIn () {
            return View ();
        }

        public IActionResult Register () {
            return View ();
        }
    }
}
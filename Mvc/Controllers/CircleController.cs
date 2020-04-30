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
    public class CircleController : Controller {

        public class CircleWallViewModel {
            public Circle Circle { get; set; }
            public List<Post> Posts { get; set; }
        }

        CircleService _circleService;
        UserService _userService;
        PostService _postService;
        public CircleController () {
            _circleService = new CircleService ();
            _userService = new UserService ();
            _postService = new PostService ();
        }

        public IActionResult Index () {
            List<Circle> circles = _circleService.GetAll ();

            return View (circles);
        }

        [Route ("Circle/{circleId}")]
        public IActionResult Index (string circleId) {
            CircleWallViewModel viewModel = new CircleWallViewModel () {
                Circle = _circleService.GetById (circleId),
                Posts = _postService.GetByCircleId (circleId),
            };

            return View ("Wall", viewModel);
        }

        [HttpGet]
        public IActionResult Create () {
            return View ();
        }

        [HttpPost]
        public IActionResult Create (Circle c) {
            c.PostIds = new List<string> ();
            c.UserIds = new List<string> ();
            _circleService.Create (c);
            return RedirectToAction ("Index");
        }

        [Route ("Circle/Subscribe/{circleId}")]
        public IActionResult Subscribe (string circleId) {
            string userId = Program.CurrentUser;
            User currentUser = _userService.GetById (userId);
            Circle circle = _circleService.GetById (circleId);
            _userService.AddCircleToUser (currentUser, circle);
            _circleService.AddUserToCircle (currentUser, circle);

            return RedirectToAction ("Index");
        }
    }
}
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

        CircleService _circleService;
        public CircleController () {
            _circleService = new CircleService ();
        }

        public IActionResult Index () {
            List<Circle> circles = _circleService.GetAll ();

            return View (circles);
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
    }
}
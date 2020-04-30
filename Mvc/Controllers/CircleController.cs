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
    }
}
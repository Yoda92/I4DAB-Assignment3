using Microsoft.AspNetCore.Mvc;

namespace Mvc.Controllers
{
    public class FeedController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
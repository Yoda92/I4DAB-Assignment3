using Microsoft.AspNetCore.Mvc;
using Data.Repositories;

namespace Mvc.Controllers
{
    public class FeedController : Controller
    {
        private FeedRepository _feedRepository;
        public FeedController()
        {
            _feedRepository = new FeedRepository();
        }
        // GET
        public IActionResult Index()
        {
            var posts = _feedRepository.GetPosts(Program.CurrentUser);
            return View(posts);
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using Requests;

namespace Mvc.Controllers
{
    public class CreatePostController : Controller
    {
        private PostService _postService;
        
        public CreatePostController()
        {
            _postService = new PostService();
        }
        
        
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            var newPost = new Post()
            {
                UserId = Program.CurrentUser,
                CircleId = request.CircleId,
                Comments = null,
                ContentType = request.ContentType,
                Text = request.Text,
                ImagePath = request.ImagePath
            };

            var result = _postService.Create(newPost);
            return RedirectToAction(request.RequestingView,request.ControllerOfRequestingView);
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Index1()
        {
            return View();
        }
        
        
    }
}

namespace Requests
{
    public class CreatePostRequest 
    {
        public string UserId { get; set; }
        public string CircleId { get; set; }
        public string ContentType { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }
        public string RequestingView { get; set; }
        public string ControllerOfRequestingView { get; set; }
    }
}

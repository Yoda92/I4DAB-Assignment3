using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using Requests;

namespace Mvc.Controllers {
    public class CreatePostController : Controller {
        private PostService _postService;
        private UserService _userService;

        public CreatePostController () {
            _postService = new PostService ();
            _userService = new UserService ();
        }

        public async Task<IActionResult> CreatePost (CreatePostRequest request) {
            var newPost = new Post () {
                UserId = Program.CurrentUser,
                UserName = Program.CurrentUser,
                CircleId = request.CircleId,
                CircleName = "Still missing lol",
                Comments = new List<Comment> (),
                ContentType = request.ContentType,
                Text = request.Text,
                ImagePath = request.ImagePath
            };

            var result = _postService.Create (newPost);
            _userService.AddPostToUser (_userService.GetById (Program.CurrentUser), result);
            return RedirectToAction (request.RequestingView, request.ControllerOfRequestingView);
        }

        public ActionResult Index () {
            return View ();
        }

        public ActionResult Index1 () {
            return View ();
        }

    }
}

namespace Requests {
    public class CreatePostRequest {
        public string UserId { get; set; }
        public string CircleId { get; set; }
        public string ContentType { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }
        public string RequestingView { get; set; }
        public string ControllerOfRequestingView { get; set; }

        public IEnumerable<ValidationResult> Validate (ValidationContext validationContext) {
            if (ContentType == "Text" && ImagePath != null) {
                yield return new ValidationResult ("A text post may not contain an image.",
                    new List<string> () { "ImagePath" });
            }
        }
    }
}
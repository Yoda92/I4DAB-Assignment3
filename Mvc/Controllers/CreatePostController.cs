using System;
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

        public async Task<IActionResult> CreatePost (string contentType, string text, string imagePath, string requestingView, string controllerOfRequestingView, string circleId, string circleName) {
            var newPost = new Post () {
                UserId = Program.CurrentUser,
                UserName = Program.CurrentUserName,
                CircleId = circleId,
                CircleName = circleName,
                Comments = new List<Comment> (),
                ContentType = contentType,
                Text = text,
                ImagePath = imagePath
            };
            Console.WriteLine(newPost);
            var result = _postService.Create (newPost);
            _userService.AddPostToUser (_userService.GetById (Program.CurrentUser), result);
            return RedirectToAction (requestingView, controllerOfRequestingView,new{circleId});
        }

        public ActionResult Index () {
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
    }
}
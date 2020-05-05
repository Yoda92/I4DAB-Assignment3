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
        private CircleService _circleService;

        public CreatePostController () {
            _postService = new PostService ();
            _userService = new UserService ();
            _circleService = new CircleService ();
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
                ImagePath = imagePath,
            };
            var result = _postService.Create (newPost);
            _userService.AddPostToUser (_userService.GetById (Program.CurrentUser), result);
            if (circleId != null) {
                Circle circle = _circleService.GetById (circleId);
                _circleService.AddPostToCircle (newPost, circle);
            }

            return RedirectToAction (requestingView, controllerOfRequestingView, new { circleId });
        }

        public ActionResult Index () {
            return View ();
        }

        [HttpPost]
        public IActionResult AddComment (CommentRequest cr) {
            var postId = cr.postId;
            Comment comment = new Comment () {
                UserId = Program.CurrentUser,
                UserName = Program.CurrentUserName,
                Text = cr.Text
            };
            var post = _postService.GetById (postId);
            _postService.AddCommentToPost (comment, post);

            if (post.CircleId != null) {
                return RedirectToAction ("Index", "Circle", new { post.CircleId });
            }

            return RedirectToAction ("Index", "Feed");
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

    public class CommentRequest {
        public string Text { get; set; }
        public string postId { get; set; }
    }
}
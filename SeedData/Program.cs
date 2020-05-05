using System;
using System.Collections.Generic;
using Data.Entities;
using Data.Services;

namespace SeedData {
    class Program {
        static void Main (string[] args) {
            var _userService = new UserService ();
            var _circleService = new CircleService ();
            var _postService = new PostService ();

            User u1 = CreateUser ("Anders", "M", 28);
            User u2 = CreateUser ("David", "M", 31);
            User u3 = CreateUser ("Christoffer", "M", 27);
            User u4 = CreateUser ("Lau", "M", 22);

            _userService.Create (u1);
            _userService.Create (u2);
            _userService.Create (u3);
            _userService.Create (u4);

            Circle c = new Circle () {
                Name = "My New Circle",
                UserIds = new List<string> (),
                PostIds = new List<string> (),
            };

            _circleService.Create (c);

            _circleService.AddUserToCircle (u1, c);
            _userService.AddCircleToUser (u1, c);
            _circleService.AddUserToCircle (u2, c);
            _userService.AddCircleToUser (u2, c);

            _userService.FollowUser (u3, u1);
            _userService.BlacklistUser (u2, u1);

            Post p1 = CreatePost (u1, c, "Text", "Hello everybody", null);
            _postService.Create (p1);
            _circleService.AddPostToCircle (p1, c);
            _userService.AddPostToUser (u1, p1);

            Post p2 = CreatePost (u1, null, "Image", "Nice picture of ocean!", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse3.mm.bing.net%2Fth%3Fid%3DOIP.X1kDFM6ei4tqszAXo5DIEAHaE9%26pid%3DApi%26h%3D180%26p%3D0&f=1");
            _postService.Create (p2);
            _userService.AddPostToUser (u1, p2);

            Post p3 = CreatePost (u2, c, "Text", "Anders why have you blacklisted me?", null);
            _postService.Create (p3);
            _circleService.AddPostToCircle (p3, c);
            _userService.AddPostToUser (u2, p3);

            Post p4 = CreatePost (u3, null, "Image", "Look at this view!", "https://forestsnews.cifor.org/wp-content/uploads/2014/12/integrated-rice-and-fish-farms-anirban-mahapatra-1024x767.jpg");
            _postService.Create (p4);
            _userService.AddPostToUser (u3, p4);

            Comment comment = new Comment () {
                UserId = p3.UserId,
                UserName = p3.UserName,
                Text = "Hi Doctor Nick!"
            };

            _postService.AddCommentToPost (comment, p1);

            System.Console.WriteLine ("=======================================");
            System.Console.WriteLine ("Done seeding data");
            System.Console.WriteLine ("Users are: Anders, David, Lau, Christoffer");
            System.Console.WriteLine ("Anders and David are part of same circle");
            System.Console.WriteLine ("David is blacklisted by Anders");
            System.Console.WriteLine ("Christoffer is followed by Anders");
            System.Console.WriteLine ("Press enter to exit");

            System.Console.ReadLine ();
        }

        private static User CreateUser (string username, string gender, int age) {
            return new User () {
                UserName = username,
                    Gender = gender,
                    Age = age,
                    PostIds = new List<string> (),
                    SubscribedCircleIds = new List<string> (),
                    FollowUserIds = new List<string> (),
                    BlackListUserIds = new List<string> ()
            };
        }

        private static Post CreatePost (
            User poster,
            Circle circle,
            string contentType,
            string text,
            string imagePath
        ) {
            return new Post () {
                UserId = poster.Id,
                    UserName = poster.UserName,
                    CircleId = circle == null ? null : circle.Id,
                    CircleName = circle == null ? null : circle.Name,
                    ContentType = contentType,
                    Text = text,
                    ImagePath = imagePath,
                    Comments = new List<Comment> (),
            };
        }
    }
}
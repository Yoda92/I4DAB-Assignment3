using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data.Services;
using MongoDB.Driver;

namespace Data.Repositories
{
    class WallRepository
    {
        private UserService _userService;
        private PostService _postService;
        public WallRepository()
        {
            _userService = new UserService();
        }
        public List<Post> GetPosts(string wallUserId, string queryUserId)
        {
            List<Post> wallPosts = new List<Post>();
            User wallUser = _userService.GetById(wallUserId);
            User queryUser = _userService.GetById(queryUserId);
            foreach(string postId in wallUser.PostIds)
            {
                Post wallUserPost = _postService.GetById(postId);
                if(wallUserPost.CircleId == null || queryUser.SubscribedCircleIds.Contains(wallUserPost.CircleId)) wallPosts.Add(wallUserPost);
            }

            return wallPosts;
        }
    }
}

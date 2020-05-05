using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data.Services;

namespace Data.Repositories
{
    public class FeedRepository
    {
        private UserService _userService;
        private PostService _postService;
        private CircleService _circleService;
        public FeedRepository()
        {
            _userService = new UserService();
            _postService = new PostService();
            _circleService = new CircleService();
        }

        public List<Post> GetPosts(string queryUserId)
        {
            List<Post> feedPosts = new List<Post>();
            User queryUser = _userService.GetById(queryUserId);

            foreach (string postId in queryUser.PostIds)
            {
                feedPosts.Add(_postService.GetById(postId));
            }

            foreach (string followId in queryUser.FollowUserIds)
            {
                if (!queryUser.BlackListUserIds.Contains(followId))
                {
                    User followUser = _userService.GetById(followId);
                    foreach (string postId in followUser.PostIds)
                    {
                        Post followUserPost = _postService.GetById(postId);
                        if (followUserPost.CircleId == null) feedPosts.Add(followUserPost);
                    }
                }
            }

            foreach (string circleId in queryUser.SubscribedCircleIds)
            {
                Circle followCircle = _circleService.GetById(circleId);
                foreach (string postId in followCircle.PostIds)
                {
                    Post followCirclePost = _postService.GetById(postId);
                    if (!queryUser.BlackListUserIds.Contains(followCirclePost.UserId)) feedPosts.Add(followCirclePost);
                }
            }

            feedPosts.Sort();
            feedPosts.Reverse();

            return feedPosts;
        }
    }
}

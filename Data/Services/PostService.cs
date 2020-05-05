using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Services;
using MongoDB.Driver;

namespace Data.Services {
    public class PostService : Service<Post> {
        public List<Post> GetByCircleId (string circleId) {
            var posts = _collection.Find (p => p.CircleId == circleId);
            return posts.ToList ();
        }

        public Post AddCommentToPost (Comment comment, Post post) {
            if (post.Comments.Contains (comment)) return post;
            post.Comments.Add (comment);
            var filter = Builders<Post>.Filter.Eq ("Id", post.Id);
            var update = Builders<Post>.Update.Set ("Comments", post.Comments);
            return post;
        }
    }
}
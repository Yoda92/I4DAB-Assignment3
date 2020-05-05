using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Services;
using MongoDB.Driver;

namespace Data.Services {
    public class PostService : Service<Post> {
        public List<Post> GetByCircleId (string circleId, string queryUserId) {
            var user = new UserService ().GetById (queryUserId);
            var posts = _collection.Find (p => p.CircleId == circleId)
                .ToEnumerable ()
                .Where (p => !user.BlackListUserIds.Contains (p.UserId))
                .Reverse ()
                .Take (6);
            return posts.ToList ();
        }

        public Post AddCommentToPost (Comment comment, Post post) {
            post.Comments.Add (comment);
            var filter = Builders<Post>.Filter.Eq ("Id", post.Id);
            var update = Builders<Post>.Update.Set ("Comments", post.Comments);
            _collection.UpdateOne (filter, update);
            return post;
        }
    }
}
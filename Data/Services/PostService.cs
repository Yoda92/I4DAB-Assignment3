using System.Collections.Generic;
using Data.Entities;
using Data.Services;
using MongoDB.Driver;

namespace Data.Services
{
    public class PostService : Service<Post>
    {
        public List<Post> GetByCircleId(string circleId)
        {
            var posts = _collection.Find(p => p.CircleId == circleId);
            return posts.ToList();
        }
    }
}
using Data.Entities;
using MongoDB.Driver;

namespace Data.Services {
    public class UserService : Service<User> {
        public User AddCircleToUser (User user, Circle circle) {
            user.SubscribedCircleIds.Add (circle.Id);
            var filter = Builders<User>.Filter.Eq ("Id", user.Id);
            var update = Builders<User>.Update.Set ("SubscribedCircleIds", user.SubscribedCircleIds);
            _collection.UpdateOne (filter, update);

            return user;
        }
    }
}
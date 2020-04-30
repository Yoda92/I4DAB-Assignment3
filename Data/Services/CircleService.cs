using Data.Entities;
using MongoDB.Driver;

namespace Data.Services {
    public class CircleService : Service<Circle> {
        public Circle AddUserToCircle (User user, Circle circle) {
            if (circle.UserIds.Contains (user.Id)) return circle; //If already subbed, do nothing
            circle.UserIds.Add (user.Id);
            var filter = Builders<Circle>.Filter.Eq ("Id", circle.Id);
            var update = Builders<Circle>.Update.Set ("UserIds", circle.UserIds);
            _collection.UpdateOne (filter, update);

            return circle;
        }
    }
}
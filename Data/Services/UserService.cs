using System;
using System.Collections.Generic;
using Data.Entities;
using MongoDB.Driver;

namespace Data.Services {
    public class UserService : Service<User> {
        public User AddCircleToUser (User user, Circle circle) {
            if (user.SubscribedCircleIds.Contains (circle.Id)) return user; //If already subbed, do nothing
            user.SubscribedCircleIds.Add (circle.Id);
            var filter = Builders<User>.Filter.Eq ("Id", user.Id);
            var update = Builders<User>.Update.Set ("SubscribedCircleIds", user.SubscribedCircleIds);
            _collection.UpdateOne (filter, update);

            return user;
        }
        public List<User> Find (string name) {
            var users = _collection.Find(user => user.UserName == name);
            return users.ToList ();
        }
    }
}
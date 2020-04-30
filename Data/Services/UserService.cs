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

        public User FollowUser (User toFollow, User follower) {
            if (follower.FollowUserIds.Contains (toFollow.Id)) return follower;

            follower.FollowUserIds.Add (toFollow.Id);
            var filter = Builders<User>.Filter.Eq ("Id", follower.Id);
            var update = Builders<User>.Update.Set ("FollowUserIds", follower.FollowUserIds);
            _collection.UpdateOne (filter, update);
            return follower;
        }

        public User BlacklistUser (User toBlacklist, User blacklister) {
            if (blacklister.BlackListUserIds.Contains (toBlacklist.Id)) return blacklister;

            blacklister.BlackListUserIds.Add (toBlacklist.Id);
            var filter = Builders<User>.Filter.Eq ("Id", blacklister.Id);
            var update = Builders<User>.Update.Set ("BlackListUserIds", blacklister.BlackListUserIds);
            _collection.UpdateOne (filter, update);
            return blacklister;
        }
    }
}
using System;
using System.Collections.Generic;
using Data.Entities;
using MongoDB.Driver;

namespace Data.Services {
    public class UserService : Service<User>
    {
        public List<User> Find(string name)
        {
            var users = _collection.Find(user => user.UserName == name);
            return users.ToList();
        }
    }
}
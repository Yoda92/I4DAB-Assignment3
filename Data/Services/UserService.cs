using System.Collections.Generic;
using MongoDB.Driver;

namespace Data {
    public class UserService : Service<User> {

        public List<User> Get () =>
        _collection.Find (user => true).ToList ();

        public User Create (User user) {
            _collection.InsertOne (user);
            return user;
        }
    }
}
using MongoDB.Driver;
using System.Collections.Generic;

namespace Project
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService()
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017/");
            var database = client.GetDatabase("FeetBook2000DB");

            _users = database.GetCollection<User>("Users");
        }

        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }
    }
}

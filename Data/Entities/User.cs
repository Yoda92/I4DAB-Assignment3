using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entities {
    public class User : Entity {
        [BsonRequired]
        public string UserName { get; set; }
        public string Gender { get; set; }

        public int Age { get; set; }

        public List<string> PostIds { get; set; }

        public List<string> SubscribedCircleIds { get; set; }

        public List<string> FollowUserIds { get; set; }

        public List<string> BlackListUserIds { get; set; }
    }
}
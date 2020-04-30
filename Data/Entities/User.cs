using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data {
    public class User : Entity {
        [BsonElement ("Name")]
        public string UserName { get; set; }
    }
}
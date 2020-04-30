using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entities {
    public class Circle : Entity {
        [BsonRequired]
        public string Name { get; set; }

        public List<string> UserIds { get; set; }

        public List<string> PostIds { get; set; }
    }
}
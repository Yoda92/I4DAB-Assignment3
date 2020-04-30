using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities {
    public class Post : Entity {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CircleId { get; set; }
        public string CircleName { get; set; }
        public string ContentType { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
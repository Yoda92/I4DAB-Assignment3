using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities {
    public class Post : Entity, IComparable<Post>
    {
        public int CompareTo(Post other)
        {
            if (null == other)
                return 1;

            return string.Compare(this.Id, other.Id);
        }
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
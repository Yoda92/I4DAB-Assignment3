using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities {
    public class Post : Entity, IValidatableObject {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CircleId { get; set; }
        public string CircleName { get; set; }
        public string ContentType { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }
        public List<Comment> Comments { get; set; }

        public IEnumerable<ValidationResult> Validate (ValidationContext validationContext) {
            if (ContentType == "Text" && ImagePath != null) {
                yield return new ValidationResult ("A text post may not contain an image.", new List<string> () { "ImagePath" });
            }
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public class UpdateCommentDto
    {
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters.")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters.")]
        public required string Title { get; set; }
       
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters.")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters.")]
        public required string Content { get; set; }
    }
}
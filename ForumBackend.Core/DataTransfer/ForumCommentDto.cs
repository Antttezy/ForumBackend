using System.ComponentModel.DataAnnotations;

namespace ForumBackend.Core.DataTransfer;

public class ForumCommentDto
{
    public int? Id { get; set; }
    
    [Required]
    public string Text { get; set; } = string.Empty;

    public DateTime? CreatedAt { get; set; }
    
    public int? Likes { get; set; }
    
    public string? Author { get; set; }
}
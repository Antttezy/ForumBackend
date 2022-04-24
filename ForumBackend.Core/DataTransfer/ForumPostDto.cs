using System.ComponentModel.DataAnnotations;

namespace ForumBackend.Core.DataTransfer;

public class ForumPostDto
{
    public int? Id { get; set; }
    
    [Required]
    [StringLength(150, MinimumLength = 1)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public string Text { get; set; } = string.Empty;
    
    public DateTime? CreatedAt { get; set; }
    
    public int? Likes { get; set; }
    
    public string? Author { get; set; }
}
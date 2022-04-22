using System.ComponentModel.DataAnnotations;

namespace ForumBackend.Core.DataTransfer;

public class PagingDto
{
    [Required]
    [Range(1, int.MaxValue)]
    public int Start { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Length { get; set; }
}
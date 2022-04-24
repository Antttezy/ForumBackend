using System.ComponentModel.DataAnnotations;

namespace ForumBackend.Core.DataTransfer;

public class PagingDto
{
    [Required]
    [Range(1, int.MaxValue)]
    public int Start { get; set; }
    
    [Required]
    [Range(1, 20)]
    public int Length { get; set; }
    
    [Range(0, long.MaxValue)]
    public long? Before { get; set; }
}
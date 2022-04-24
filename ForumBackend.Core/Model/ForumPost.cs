using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumBackend.Core.Model;

public class ForumPost
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 1)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Text { get; set; } = string.Empty;

    [Required]
    public long CreatedAt { get; set; }

    [Required]
    [ForeignKey(nameof(Author))]
    public int AuthorRef { get; set; }

#pragma warning disable
    [Required]
    public ForumUser Author { get; set; }
#pragma warning restore
}
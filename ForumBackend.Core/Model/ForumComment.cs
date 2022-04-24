using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumBackend.Core.Model;

public class ForumComment
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(Post))]
    public int PostRef { get; set; }

#pragma warning disable

    [Required]
    public ForumPost Post { get; set; }
#pragma warning restore

    [Required]
    [ForeignKey(nameof(Author))]
    public int AuthorRef { get; set; }

#pragma warning disable

    [Required]
    public ForumUser Author { get; set; }
#pragma warning restore

    [Required]
    public long CreatedAt { get; set; }

    [Required]
    public string Text { get; set; } = string.Empty;

#pragma warning disable

    public ICollection<CommentLike> LikedUsers { get; set; }
#pragma warning restore
}
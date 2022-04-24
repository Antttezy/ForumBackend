using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumBackend.Core.Model;

public class PostLike
{
    [Key]
    public int Id { get; set; }

#pragma warning disable

    [Required]
    [ForeignKey(nameof(Post))]
    public int PostRef { get; set; }

    [Required]
    public ForumPost Post { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public int UserRef { get; set; }

    [Required]
    public ForumUser User { get; set; }
#pragma warning restore
}
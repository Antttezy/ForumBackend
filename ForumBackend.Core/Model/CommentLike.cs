using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumBackend.Core.Model;

public class CommentLike
{
    [Key]
    public int Id { get; set; }

#pragma warning disable

    [Required]
    [ForeignKey(nameof(Comment))]
    public int CommentRef { get; set; }

    [Required]
    public ForumComment Comment { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public int UserRef { get; set; }

    [Required]
    public ForumUser User { get; set; }
#pragma warning restore
}
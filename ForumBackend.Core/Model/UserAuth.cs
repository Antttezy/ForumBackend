using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumBackend.Core.Model;

public class UserAuth
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(User))]
    [Required]
    public int UserRef { get; set; }

#pragma warning disable
    [Required]
    public ForumUser User { get; set; }
#pragma warning restore

    [Required]
    public string Password { get; set; } = string.Empty;
}
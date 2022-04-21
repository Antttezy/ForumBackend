using System.ComponentModel.DataAnnotations;

namespace ForumBackend.Core.Model;

public class ForumUser
{
    [Key]
    public int Id { get; set; }

    [StringLength(127, MinimumLength = 3)]
    [Required]
    public string Nickname { get; set; } = string.Empty;

    [StringLength(127, MinimumLength = 3)]
    [Required]
    public string Email { get; set; } = string.Empty;
}
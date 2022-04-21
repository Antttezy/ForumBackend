using System.ComponentModel.DataAnnotations;

namespace ForumBackend.Core.DataTransfer;

public class ForumUserDto
{
    [StringLength(127, MinimumLength = 3)]
    [Required]
    public string Nickname { get; set; } = string.Empty;

    [StringLength(127, MinimumLength = 3)]
    [Required]
    public string Email { get; set; } = string.Empty;

    [StringLength(32, MinimumLength = 6)]
    [Required]
    public string Password { get; set; } = string.Empty;
}
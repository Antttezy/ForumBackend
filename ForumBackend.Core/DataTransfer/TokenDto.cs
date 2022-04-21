using System.ComponentModel.DataAnnotations;

namespace ForumBackend.Core.DataTransfer;

public class TokenDto
{
    [Required]
    public string Token { get; set; } = string.Empty;
}
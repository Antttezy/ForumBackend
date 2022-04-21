using Microsoft.IdentityModel.Tokens;

namespace ForumBackend.Core.Objects;

public class AccessTokenParameters
{
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public TimeSpan Lifetime { get; init; } = TimeSpan.Zero;
    public RsaSecurityKey? SecurityKey { get; init; } = null;
}
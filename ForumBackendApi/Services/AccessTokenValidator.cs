using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ForumBackend.Core.Objects;
using ForumBackend.Core.Services;

namespace ForumBackendApi.Services;

public class AccessTokenValidator : IAccessTokenValidator
{
    private readonly AccessTokenParameters _parameters;

    public AccessTokenValidator(AccessTokenParameters parameters)
    {
        _parameters = parameters;
    }

    public ClaimsIdentity? ValidateToken(string token)
    {
        JwtSecurityTokenHandler handler = new();

        try
        {
            ClaimsPrincipal principal = handler.ValidateToken(token, new()
            {
                ValidIssuer = _parameters.Issuer,
                ValidateIssuer = true,
                ValidAudience = _parameters.Audience,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = _parameters.SecurityKey
            }, out _);

            if (principal.Identity is ClaimsIdentity identity)
            {
                return identity;
            }

            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
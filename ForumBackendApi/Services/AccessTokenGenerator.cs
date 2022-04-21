using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ForumBackend.Core.Model;
using ForumBackend.Core.Objects;
using ForumBackend.Core.Services;
using Microsoft.IdentityModel.Tokens;

namespace ForumBackendApi.Services;

public class AccessTokenGenerator : IAccessTokenGenerator
{
    private readonly AccessTokenParameters _parameters;

    public AccessTokenGenerator(AccessTokenParameters parameters)
    {
        _parameters = parameters;
    }

    public string GetToken(ForumUser user)
    {
        var claims = new List<Claim>()
        {
            new("id", user.Id.ToString())
        };

        SecurityTokenDescriptor token = new()
        {
            Subject = new(claims),
            Issuer = _parameters.Issuer,
            Audience = _parameters.Audience,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.Add(_parameters.Lifetime),
            SigningCredentials = new(_parameters.SecurityKey, SecurityAlgorithms.RsaSha512Signature)
        };

        JwtSecurityTokenHandler handler = new();
        SecurityToken jwt = handler.CreateToken(token);
        return handler.WriteToken(jwt);
    }
}
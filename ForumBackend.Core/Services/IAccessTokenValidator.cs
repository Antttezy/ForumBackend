using System.Security.Claims;

namespace ForumBackend.Core.Services;

public interface IAccessTokenValidator
{
    ClaimsIdentity? ValidateToken(string token);
}
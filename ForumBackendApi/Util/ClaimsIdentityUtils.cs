using System.Security.Claims;

namespace ForumBackendApi.Util;

public static class ClaimsIdentityUtils
{
    public static int? GetUserId(this ClaimsIdentity identity)
    {
        var id = identity.FindFirst("id")?.Value;
        return id == null ? null : int.Parse(id);
    }
}
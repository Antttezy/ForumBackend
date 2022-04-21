using ForumBackend.Core.Model;

namespace ForumBackend.Core.Services;

public interface IAccessTokenGenerator
{
    public string GetToken(ForumUser user);
}
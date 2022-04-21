using ForumBackend.Core.DataTransfer;
namespace ForumBackend.Core.Services;

public interface IAuthenticationService
{
    Task<ResultBase<string, string>> Login(string username, string password);
    Task<ResultBase<string, string>> Regenerate(string token);
}
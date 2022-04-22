using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Model;

namespace ForumBackend.Core.Services;

public interface IUserService
{
     Task<ResultBase<ForumUser, string>> RegisterUser(string nickname, string email, string password);
     Task<ForumUser?> GetUserById(int id);
}
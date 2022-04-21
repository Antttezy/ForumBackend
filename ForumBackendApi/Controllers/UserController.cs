using ForumBackend.Core.DataTransfer;
using Microsoft.AspNetCore.Mvc;

namespace ForumBackendApi.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    [HttpGet("user/me")]
    public async Task<ResultBase<string, string>> GetUserInfo()
    {
        var result = new SuccessResult<string, string>("success");
        return result;
    }
}
using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForumBackendApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController: ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<ResultBase<string, string>> Login([FromBody] LoginDto login)
    {
        var result = await _authenticationService.Login(login.Username, login.Password);
        return result;
    }
    
    [HttpPost("regenerate")]
    public async Task<ResultBase<string, string>> Regenerate([FromBody] TokenDto body)
    {
        var result = await _authenticationService.Regenerate(body.Token);
        return result;
    }
}
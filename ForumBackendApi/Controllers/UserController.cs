using AutoMapper;
using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Model;
using ForumBackend.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForumBackendApi.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    [HttpGet("me")]
    public async Task<ResultBase<string, string>> GetUserInfo()
    {
        throw new NotImplementedException();
    }

    [HttpPost("register")]
    public async Task<ResultBase<ForumUserDto, string>> Register([FromBody] ForumUserDto registerBody)
    {
        var registerResult = await _userService.RegisterUser(
            registerBody.Nickname,
            registerBody.Email,
            registerBody.Password
        );

        if (!registerResult.IsSuccess())
        {
            if (registerResult is ErrorResult<ForumUser, string> error)
            {
                return new ErrorResult<ForumUserDto, string>(error.GetError());
            }

            return new ErrorResult<ForumUserDto, string>("Unknown error, please try again");
        }

        ForumUserDto dto = _mapper.Map<ForumUserDto>(registerResult.GetResult());
        return new SuccessResult<ForumUserDto, string>(dto);
    }
}
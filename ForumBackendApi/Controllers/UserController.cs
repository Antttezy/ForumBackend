using System.Security.Claims;
using AutoMapper;
using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Model;
using ForumBackend.Core.Services;
using ForumBackendApi.Util;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpGet("me")]
    public async Task<ResultBase<ForumUserDto, string>> GetSelfInfo()
    {
        if (User.Identity is not ClaimsIdentity identity)
        {
            return new ErrorResult<ForumUserDto, string>("Bad identity");
        }

        var id = identity.GetUserId();

        if (id is not { } identifier)
        {
            return new ErrorResult<ForumUserDto, string>("Bad token");
        }

        ForumUser? user = await _userService.GetUserById(identifier);

        if (user == null)
        {
            return new ErrorResult<ForumUserDto, string>("User not found");
        }

        return new SuccessResult<ForumUserDto, string>(_mapper.Map<ForumUserDto>(user));
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
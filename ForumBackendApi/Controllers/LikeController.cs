using System.Security.Claims;
using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Services;
using ForumBackendApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumBackendApi.Controllers;

[ApiController]
[Route("likes")]
public class LikeController : ControllerBase
{
    private readonly ILikeService _likeService;

    public LikeController(ILikeService likeService)
    {
        _likeService = likeService;
    }

    [Authorize]
    [HttpPost("likePost")]
    public async Task<ResultBase<string, string>> LikePost([FromQuery(Name = "id")] int postId)
    {
        if (User.Identity is not ClaimsIdentity identity)
        {
            return new ErrorResult<string, string>("Bad identity");
        }

        var id = identity.GetUserId();

        if (id is not { } identifier)
        {
            return new ErrorResult<string, string>("Bad token");
        }

        var result = await _likeService.LikePost(postId, identifier);
        return result;
    }

    [Authorize]
    [HttpPost("likePostRetract")]
    public async Task<ResultBase<string, string>> LikePostRetract([FromQuery(Name = "id")] int postId)
    {
        if (User.Identity is not ClaimsIdentity identity)
        {
            return new ErrorResult<string, string>("Bad identity");
        }

        var id = identity.GetUserId();

        if (id is not { } identifier)
        {
            return new ErrorResult<string, string>("Bad token");
        }

        var result = await _likeService.LikePostRetract(postId, identifier);
        return result;
    }

    [Authorize]
    [HttpPost("likeComment")]
    public async Task<ResultBase<string, string>> LikeComment([FromQuery(Name = "id")] int commentId)
    {
        if (User.Identity is not ClaimsIdentity identity)
        {
            return new ErrorResult<string, string>("Bad identity");
        }

        var id = identity.GetUserId();

        if (id is not { } identifier)
        {
            return new ErrorResult<string, string>("Bad token");
        }

        var result = await _likeService.LikeComment(commentId, identifier);
        return result;
    }

    [Authorize]
    [HttpPost("likeCommentRetract")]
    public async Task<ResultBase<string, string>> LikeCommentRetract([FromQuery(Name = "id")] int commentId)
    {
        if (User.Identity is not ClaimsIdentity identity)
        {
            return new ErrorResult<string, string>("Bad identity");
        }

        var id = identity.GetUserId();

        if (id is not { } identifier)
        {
            return new ErrorResult<string, string>("Bad token");
        }

        var result = await _likeService.LikeCommentRetract(commentId, identifier);
        return result;
    }
}
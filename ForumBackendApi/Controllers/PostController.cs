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
[Route("posts")]
public class PostController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPostService _postService;
    private readonly ICommentService _commentService;

    public PostController(IMapper mapper, IPostService postService, ICommentService commentService)
    {
        _mapper = mapper;
        _postService = postService;
        _commentService = commentService;
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<ResultBase<ForumPostDto, string>> CreatePost([FromBody] ForumPostDto newPost)
    {
        if (User.Identity is not ClaimsIdentity identity)
        {
            return new ErrorResult<ForumPostDto, string>("Bad identity");
        }

        var id = identity.GetUserId();

        if (id is not { } identifier)
        {
            return new ErrorResult<ForumPostDto, string>("Bad token");
        }

        var result = await _postService.CreatePost(newPost.Description, newPost.Text, identifier);

        if (result is ErrorResult<ForumPost, string> err)
        {
            return new ErrorResult<ForumPostDto, string>(err.GetError());
        }

        return new SuccessResult<ForumPostDto, string>(_mapper.Map<ForumPostDto>(result.GetResult()));
    }

    [HttpGet("list")]
    public async Task<IEnumerable<ForumPostDto>> ListPosts([FromQuery] PagingDto paging)
    {
        var posts = (await _postService.ListPosts(paging.Start, paging.Length, paging.Before)).ToList();
        var id = (User.Identity as ClaimsIdentity)?.GetUserId();

        return _mapper.ProjectTo<ForumPostDto>(posts.AsQueryable())
            .Select(p => new ForumPostDto
            {
                Id = p.Id,
                Author = p.Author,
                Text = null!,
                Description = p.Description,
                Likes = p.Likes,
                CreatedAt = p.CreatedAt,
                Liked = posts.First(post => post.Id == p.Id).LikedUsers.Any(u => u.UserRef == id)
            });
    }

    [HttpGet("details/{id:int}")]
    public async Task<ResultBase<ForumPostDto, string>> PostDetails([FromRoute] int id)
    {
        ForumPost? post = await _postService.PostDetails(id);

        if (post is not { } p)
        {
            return new ErrorResult<ForumPostDto, string>("Post not found");
        }

        return new SuccessResult<ForumPostDto, string>(_mapper.Map<ForumPostDto>(p));
    }

    [HttpGet("{id:int}/comments")]
    public async Task<IEnumerable<ForumCommentDto>> PostDetails([FromRoute] int id, [FromQuery] PagingDto pagingDto)
    {
        var comments = (await _commentService.GetPostComments(id, pagingDto.Start, pagingDto.Length)).ToList();
        var userId = (User.Identity as ClaimsIdentity)?.GetUserId();

        return _mapper.ProjectTo<ForumCommentDto>(comments.AsQueryable())
            .Select(c => new ForumCommentDto
            {
                Id = c.Id,
                Likes = c.Likes,
                Author = c.Author,
                Text = c.Text,
                CreatedAt = c.CreatedAt,
                Liked = comments.First(com => com.Id == c.Id).LikedUsers.Any(u => u.UserRef == userId)
            });
    }

    [Authorize]
    [HttpPost("{id:int}/comments/create")]
    public async Task<ResultBase<ForumCommentDto, string>> LeaveComment([FromRoute] int id,
        [FromBody] ForumCommentDto comment)
    {
        if (User.Identity is not ClaimsIdentity identity)
        {
            return new ErrorResult<ForumCommentDto, string>("Bad identity");
        }

        var userId = identity.GetUserId();

        if (userId is not { } identifier)
        {
            return new ErrorResult<ForumCommentDto, string>("Bad token");
        }

        var result = await _commentService.CreateComment(id, identifier, comment.Text);

        if (result is ErrorResult<ForumComment, string> errorResult)
        {
            return new ErrorResult<ForumCommentDto, string>(errorResult.Error);
        }

        return new SuccessResult<ForumCommentDto, string>(_mapper.Map<ForumCommentDto>(result.GetResult()));
    }
}
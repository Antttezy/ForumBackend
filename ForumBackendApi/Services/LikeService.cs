using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Model;
using ForumBackend.Core.Services;
using ForumBackend.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ForumBackendApi.Services;

public class LikeService: ILikeService
{
    private readonly ForumContext _context;

    public LikeService(ForumContext context)
    {
        _context = context;
    }
    
    public async Task<ResultBase<string, string>> LikePost(int postId, int userId)
    {
        ForumUser? user = await _context.Users!
            .Include(u => u.LikedPosts)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
        {
            return new ErrorResult<string, string>("User not found");
        }
        
        ForumPost? post = await _context.Posts!
            .Include(p => p.LikedUsers)
            .FirstOrDefaultAsync(p => p.Id == postId);
        
        if (post is null)
        {
            return new ErrorResult<string, string>("Post not found");
        }

        PostLike? like = post.LikedUsers
            .FirstOrDefault(u => u.UserRef == user.Id);

        if (like is not null)
        {
            return new ErrorResult<string, string>("You have already liked this post");
        }

        like = new()
        {
            Post = post,
            PostRef = post.Id,
            User = user,
            UserRef = user.Id
        };

        try
        {
            post.LikedUsers.Add(like);
            await _context.SaveChangesAsync();
            return new SuccessResult<string, string>("Post was liked");
        }
        catch (DbUpdateException)
        {
            return new ErrorResult<string, string>("An error occured, please try again later");
        }
    }

    public async Task<ResultBase<string, string>> LikePostRetract(int postId, int userId)
    {
        ForumUser? user = await _context.Users!
            .Include(u => u.LikedPosts)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
        {
            return new ErrorResult<string, string>("User not found");
        }
        
        ForumPost? post = await _context.Posts!
            .Include(p => p.LikedUsers)
            .FirstOrDefaultAsync(p => p.Id == postId);
        
        if (post is null)
        {
            return new ErrorResult<string, string>("Post not found");
        }

        PostLike? like = post.LikedUsers
            .FirstOrDefault(u => u.UserRef == user.Id);

        if (like is null)
        {
            return new ErrorResult<string, string>("You have not liked this post yet");
        }

        try
        {
            post.LikedUsers.Remove(like);
            await _context.SaveChangesAsync();
            return new SuccessResult<string, string>("Like was retracted");
        }
        catch (DbUpdateException)
        {
            return new ErrorResult<string, string>("An error occured, please try again later");
        }
    }

    public async Task<ResultBase<string, string>> LikeComment(int commentId, int userId)
    {
        ForumUser? user = await _context.Users!
            .Include(u => u.LikedPosts)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
        {
            return new ErrorResult<string, string>("User not found");
        }
        
        ForumComment? comment = await _context.Comments!
            .Include(c => c.LikedUsers)
            .FirstOrDefaultAsync(p => p.Id == commentId);
        
        if (comment is null)
        {
            return new ErrorResult<string, string>("Comment not found");
        }

        CommentLike? like = comment.LikedUsers
            .FirstOrDefault(u => u.UserRef == user.Id);

        if (like is not null)
        {
            return new ErrorResult<string, string>("You have already liked this comment");
        }

        like = new()
        {
            Comment = comment,
            CommentRef = comment.Id,
            User = user,
            UserRef = user.Id
        };

        try
        {
            comment.LikedUsers.Add(like);
            await _context.SaveChangesAsync();
            return new SuccessResult<string, string>("Comment was liked");
        }
        catch (DbUpdateException)
        {
            return new ErrorResult<string, string>("An error occured, please try again later");
        }
    }

    public async Task<ResultBase<string, string>> LikeCommentRetract(int commentId, int userId)
    {
        ForumUser? user = await _context.Users!
            .Include(u => u.LikedPosts)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
        {
            return new ErrorResult<string, string>("User not found");
        }
        
        ForumComment? comment = await _context.Comments!
            .Include(p => p.LikedUsers)
            .FirstOrDefaultAsync(p => p.Id == commentId);
        
        if (comment is null)
        {
            return new ErrorResult<string, string>("Post not found");
        }

        CommentLike? like = comment.LikedUsers
            .FirstOrDefault(u => u.UserRef == user.Id);

        if (like is null)
        {
            return new ErrorResult<string, string>("You have not liked this comment yet");
        }

        try
        {
            comment.LikedUsers.Remove(like);
            await _context.SaveChangesAsync();
            return new SuccessResult<string, string>("Like was retracted");
        }
        catch (DbUpdateException)
        {
            return new ErrorResult<string, string>("An error occured, please try again later");
        }
    }
}
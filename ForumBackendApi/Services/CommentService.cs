using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Model;
using ForumBackend.Core.Services;
using ForumBackend.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ForumBackendApi.Services;

public class CommentService : ICommentService
{
    private readonly ForumContext _context;

    public CommentService(ForumContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ForumComment>> GetPostComments(int id, int start, int length)
    {
        var comments = await _context.Comments!
            .Include(c => c.Post)
            .Include(c => c.Author)
            .Include(c => c.LikedUsers)
            .Where(c => c.PostRef == id)
            .OrderBy(c => c.CreatedAt)
            .Skip(start - 1)
            .Take(length)
            .ToListAsync();

        return comments;
    }

    public async Task<ResultBase<ForumComment, string>> CreateComment(int id, int authorId, string text)
    {
        ForumPost? post = await _context.Posts!
            .AsTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
        {
            return new ErrorResult<ForumComment, string>("Post not found");
        }

        ForumUser? user = await _context.Users!
            .AsTracking()
            .FirstOrDefaultAsync(u => u.Id == authorId);

        if (user == null)
        {
            return new ErrorResult<ForumComment, string>("User not found");
        }

        ForumComment comment = new()
        {
            Author = user,
            Post = post,
            CreatedAt = (long) Math.Floor((DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds),
            AuthorRef = user.Id,
            PostRef = post.Id,
            Text = text
        };

        try
        {
            await _context.Comments!.AddAsync(comment);
            await _context.SaveChangesAsync();

            return new SuccessResult<ForumComment, string>(comment);
        }
        catch (DbUpdateException)
        {
            return new ErrorResult<ForumComment, string>("Could not leave a comment. Try again");
        }
    }
}
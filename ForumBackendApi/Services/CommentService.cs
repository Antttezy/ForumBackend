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
            .Where(c => c.PostRef == id)
            .OrderBy(c => c.CreatedAt)
            .Skip(start - 1)
            .Take(length)
            .ToListAsync();

        return comments;
    }
}
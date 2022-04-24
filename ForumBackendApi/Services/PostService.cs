using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Model;
using ForumBackend.Core.Services;
using ForumBackend.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ForumBackendApi.Services;

public class PostService : IPostService
{
    private readonly ForumContext _context;

    public PostService(ForumContext context)
    {
        _context = context;
    }

    public async Task<ResultBase<ForumPost, string>> CreatePost(string description, string text, int authorId)
    {
        ForumUser? user = await _context.Users!
            .FirstOrDefaultAsync(u => u.Id == authorId);

        if (user == null)
        {
            return new ErrorResult<ForumPost, string>("User not found");
        }

        ForumPost newPost = new()
        {
            Description = description,
            Text = text,
            CreatedAt = (long) Math.Floor((DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds),
            Author = user
        };

        try
        {
            await _context.Posts!.AddAsync(newPost);
            await _context.SaveChangesAsync();

            return new SuccessResult<ForumPost, string>(newPost);
        }
        catch (DbUpdateException)
        {
            return new ErrorResult<ForumPost, string>(
                "An unexpected error occured while creating new post, please try again");
        }
    }

    public async Task<IEnumerable<ForumPost>> ListPosts(int start, int length, long? before = null)
    {
        IQueryable<ForumPost> posts = _context.Posts!
            .AsNoTracking()
            .Include(p => p.Author);
        // TODO: Include likes counter

        if (before is { } bf)
        {
            posts = posts.Where(p => p.CreatedAt < bf);
        }

        posts = posts
            .OrderByDescending(p => p.CreatedAt)
            .Skip(start - 1)
            .Take(length);

        return await posts.ToListAsync();
    }

    public async Task<ForumPost?> PostDetails(int id)
    {
        ForumPost? post = await _context.Posts!
            .AsNoTracking()
            .Include(p => p.Author)
            // TODO: Include likes counter
            .FirstOrDefaultAsync(p => p.Id == id);

        return post;
    }
}
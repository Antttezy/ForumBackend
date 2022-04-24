using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Model;

namespace ForumBackend.Core.Services;

public interface IPostService
{
    Task<ResultBase<ForumPost, string>> CreatePost(string description, string text, int authorId);
    Task<IEnumerable<ForumPost>> ListPosts(int start, int length, long? before = null);
    Task<ForumPost?> PostDetails(int id);
}
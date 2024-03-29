using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Model;

namespace ForumBackend.Core.Services;

public interface ICommentService
{
    Task<IEnumerable<ForumComment>> GetPostComments(int id, int start, int length);
    Task<ResultBase<ForumComment, string>> CreateComment(int id, int authorId, string text);
}
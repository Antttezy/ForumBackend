using ForumBackend.Core.DataTransfer;

namespace ForumBackend.Core.Services;

public interface ILikeService
{
    Task<ResultBase<string, string>> LikePost(int postId, int userId);
    Task<ResultBase<string, string>> LikePostRetract(int postId, int userId);
    Task<ResultBase<string, string>> LikeComment(int commentId, int userId);
    Task<ResultBase<string, string>> LikeCommentRetract(int commentId, int userId);
}
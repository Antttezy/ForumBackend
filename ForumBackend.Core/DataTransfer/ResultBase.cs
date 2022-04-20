namespace ForumBackend.Core.DataTransfer;

public abstract class ResultBase<TResult, TError> where TResult : class where TError : class
{
    internal ResultBase()
    {
    }

    public abstract TResult? GetResult();
    public abstract TError? GetError();
    public abstract bool IsSuccess();

    public static SuccessResult<TResult, TError> CreateSuccess(TResult result)
    {
        return new(result);
    }

    public static ErrorResult<TResult, TError> CreateError(TError error)
    {
        return new(error);
    }
}
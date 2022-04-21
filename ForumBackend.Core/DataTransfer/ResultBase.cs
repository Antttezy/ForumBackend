namespace ForumBackend.Core.DataTransfer;

public abstract class ResultBase<TResult, TError> where TResult : class where TError : class
{
    internal ResultBase()
    {
    }

    public abstract TResult? GetResult();
    public abstract TError? GetError();
    public abstract bool IsSuccess();
}
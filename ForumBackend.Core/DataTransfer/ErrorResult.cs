namespace ForumBackend.Core.DataTransfer;

public sealed class ErrorResult<TResult, TError> : ResultBase<TResult, TError> where TResult: class where TError: class
{
    public ErrorResult(TError error)
    {
        Error = error;
    }

    public TError Error {get;}
    
    public override TResult? GetResult()
    {
        return null;
    }

    public override TError GetError()
    {
        return Error;
    }

    public override bool IsSuccess()
    {
        return false;
    }
}
namespace ForumBackend.Core.DataTransfer;

public sealed class SuccessResult<TResult, TError> : ResultBase<TResult, TError> where TResult: class where TError: class
{
    internal SuccessResult(TResult result)
    {
        Result = result;
    }
    
    public TResult Result { get; }
    
    public override TResult GetResult()
    {
        return Result;
    }

    public override TError? GetError()
    {
        return null;
    }

    public override bool IsSuccess()
    {
        return true;
    }
}
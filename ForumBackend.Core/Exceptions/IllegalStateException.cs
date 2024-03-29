using System.Runtime.Serialization;

namespace ForumBackend.Core.Exceptions;

public class IllegalStateException : Exception
{
    public IllegalStateException()
    {
    }

    protected IllegalStateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public IllegalStateException(string? message) : base(message)
    {
    }

    public IllegalStateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
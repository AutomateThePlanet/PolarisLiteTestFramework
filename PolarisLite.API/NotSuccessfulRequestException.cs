namespace PolarisLite.API;
[Serializable]
public class NotSuccessfulRequestException : Exception
{
    public NotSuccessfulRequestException()
    {
    }

    public NotSuccessfulRequestException(string message)
        : base(message)
    {
    }

    public NotSuccessfulRequestException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

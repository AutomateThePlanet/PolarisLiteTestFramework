namespace Polaris.Plugins.Common.ExecutionTime;

public class ExecutionTimeoutException : Exception
{
    public ExecutionTimeoutException()
    {
    }

    public ExecutionTimeoutException(string message)
        : base(message)
    {
    }

    public ExecutionTimeoutException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
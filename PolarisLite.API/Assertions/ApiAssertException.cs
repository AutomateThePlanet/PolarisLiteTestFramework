namespace PolarisLite.API;

public class ApiAssertException : Exception
{
    public ApiAssertException()
    {
    }

    public ApiAssertException(string message)
        : base(message)
    {
    }

    public ApiAssertException(string message, string url)
        : base(FormatExceptionMessage(message, url))
    {
    }

    public ApiAssertException(string message, string url, Exception inner)
        : base(FormatExceptionMessage(message, url), inner)
    {
    }

    private static string FormatExceptionMessage(string exceptionMessage, string url) => $"\n\n{new string('#', 40)}\n\n{exceptionMessage} Request against URL- {url}\n\n{new string('#', 40)}\n";
}

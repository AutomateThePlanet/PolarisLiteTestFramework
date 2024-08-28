namespace PolarisLite.Web.Plugins;

public class LighthouseAssertFailedException : Exception
{
    public LighthouseAssertFailedException()
    {
    }

    public LighthouseAssertFailedException(string message)
        : base(FormatExceptionMessage(message))
    {
    }

    public LighthouseAssertFailedException(string message, Exception inner)
        : base(FormatExceptionMessage(message), inner)
    {
    }

    private static string FormatExceptionMessage(string exceptionMessage) => $"\n\n{new string('#', 40)}\n\n{exceptionMessage}\n\n{new string('#', 40)}\n";
}

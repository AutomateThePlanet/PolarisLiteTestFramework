namespace PolarisLite.Core.Layout;

public class LayoutAssertFailedException : Exception
{
    public LayoutAssertFailedException()
    {
    }

    public LayoutAssertFailedException(string message)
        : base(FormatExceptionMessage(message))
    {
    }

    public LayoutAssertFailedException(string message, Exception inner)
        : base(FormatExceptionMessage(message), inner)
    {
    }

    private static string FormatExceptionMessage(string exceptionMessage) => $"\n\n{new string('#', 40)}\n\n{exceptionMessage}\n\n{new string('#', 40)}\n";
}

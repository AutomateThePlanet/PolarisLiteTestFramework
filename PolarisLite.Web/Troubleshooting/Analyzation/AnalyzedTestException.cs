namespace PolarisLite.Web.Troubshoting;

[Serializable]
public class AnalyzedTestException : Exception
{
    public AnalyzedTestException()
    {
    }

    public AnalyzedTestException(string message)
        : base(FormatExceptionMessage(message))
    {
    }

    public AnalyzedTestException(string message, string url, Exception inner)
        : base(FormatExceptionMessage(message, url), inner)
    {
    }

    private static string FormatExceptionMessage(string exceptionMessage)
    {
        return $"{Environment.NewLine}{Environment.NewLine}{new string('#', 40)}{Environment.NewLine}{Environment.NewLine}{exceptionMessage}{Environment.NewLine}{Environment.NewLine}{new string('#', 40)}{Environment.NewLine}";
    }

    private static string FormatExceptionMessage(string exceptionMessage, string url)
    {
        return $"{Environment.NewLine}{Environment.NewLine}{new string('#', 40)}{Environment.NewLine}{Environment.NewLine}{exceptionMessage}{Environment.NewLine}{Environment.NewLine}{new string('#', 40)}{Environment.NewLine} URL: {url}";
    }
}

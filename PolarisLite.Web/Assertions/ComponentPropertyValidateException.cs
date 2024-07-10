namespace PolarisLite.Web.Assertions;

public class ComponentPropertyValidateException : Exception
{
    public ComponentPropertyValidateException()
    {
    }

    public ComponentPropertyValidateException(string message)
        : base(message)
    {
    }

    public ComponentPropertyValidateException(string message, string url)
        : base($"{message} The test failed on URL: {url}")
    {
    }

    public ComponentPropertyValidateException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
namespace PolarisLite.Mobile.Assertions;
public class ComponentPropertyValidateException : Exception
{
    public ComponentPropertyValidateException()
    {
    }

    public ComponentPropertyValidateException(string message)
        : base(message)
    {
    }

    public ComponentPropertyValidateException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

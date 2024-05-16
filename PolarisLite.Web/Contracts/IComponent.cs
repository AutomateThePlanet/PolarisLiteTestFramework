namespace PolarisLite.Web.Contracts;
public interface IComponent
{
    public IWebElement WrappedElement { get; internal set; }

    public string GetAttribute(string attributeName)
    {
        return WrappedElement?.GetAttribute(attributeName);
    }
}

namespace PolarisLite.Web.Contracts;

public interface IComponentText : IComponent
{
    public void TypeText(string text)
    {
        WrappedElement?.Clear();
        WrappedElement?.SendKeys(text);
    }
}
namespace PolarisLite.Web.Contracts;

public interface IComponentInnerText : IComponent
{
    public string Text => WrappedElement?.Text;
}
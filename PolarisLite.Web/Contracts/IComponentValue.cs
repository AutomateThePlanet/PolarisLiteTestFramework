namespace PolarisLite.Web.Contracts;

public interface IComponentValue : IComponent
{
    public string Value => GetAttribute("value");
}
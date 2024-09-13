namespace PolarisLite.Web.Contracts;

public interface IComponentText : IComponent
{
    void TypeText(string text, InfoType infoType = InfoType.INFO);
}
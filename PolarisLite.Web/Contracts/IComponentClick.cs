namespace PolarisLite.Web.Contracts;
internal interface IComponentClick : IComponent
{
    public void Click()
    {
        WrappedElement?.Click();
    }
}

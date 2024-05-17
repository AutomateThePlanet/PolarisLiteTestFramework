namespace PolarisLite.Web.Contracts;

public interface IComponentDisabled : IComponent
{
    bool IsDisabled { get; }
}

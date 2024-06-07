namespace PolarisLite.Mobile.Contracts;

public interface IComponentDisabled : IComponent
{
    bool IsDisabled { get; }
}

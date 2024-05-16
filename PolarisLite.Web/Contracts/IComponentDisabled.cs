namespace PolarisLite.Web.Contracts;

internal interface IComponentDisabled : IComponent
{
    internal bool IsDisabled => bool.Parse(GetAttribute("disabled"));
}

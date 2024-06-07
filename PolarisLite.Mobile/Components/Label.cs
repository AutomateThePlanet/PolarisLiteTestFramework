using PolarisLite.Mobile.Contracts;

namespace PolarisLite.Mobile.Components;

public class Label : AndroidComponent, IComponentText
{
    public string Text => base.GetText();
}
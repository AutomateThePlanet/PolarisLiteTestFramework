using PolarisLite.Web.Contracts;

namespace PolarisLite.Web.Events;
public class ComponentActionEventArgs
{
    public ComponentActionEventArgs(IComponent element) => Element = element;

    public ComponentActionEventArgs(IComponent element, string actionValue)
        : this(element) => ActionValue = actionValue;

    public IComponent Element { get; }
    public string ActionValue { get; }
}

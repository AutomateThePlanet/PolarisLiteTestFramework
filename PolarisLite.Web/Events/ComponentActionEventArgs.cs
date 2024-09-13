using PolarisLite.Core.Settings.StaticSettings;
using PolarisLite.Web.Contracts;

namespace PolarisLite.Web.Events;
public class ComponentActionEventArgs
{
    private string _actionValue;
    public ComponentActionEventArgs(IComponent element)
    {
        Element = element;
    }

    public ComponentActionEventArgs(IComponent element, string actionValue, InfoType infoType = InfoType.INFO)
    : this(element)
    {
        ActionValue = actionValue;
        InfoType = infoType;
    }

    public IComponent Element { get; }
    public string ActionValue 
    {
        set
        {
            _actionValue = value;
        }
        get
        {
            return GlobalSettings.LoggingSettings.ShouldMaskSensitiveInfo ? "********" : _actionValue;
        }
    }
    public InfoType InfoType { get; }
}

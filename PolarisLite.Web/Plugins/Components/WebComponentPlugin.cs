using PolarisLite.Core.Plugins;
using PolarisLite.Web.Components;
using System.Reflection;

namespace PolarisLite.Web;
public class WebComponentPlugin
{
    public virtual void OnComponentFound(WebComponent component) { }
    public virtual void OnComponentsFound(List<WebComponent> components) { }
}

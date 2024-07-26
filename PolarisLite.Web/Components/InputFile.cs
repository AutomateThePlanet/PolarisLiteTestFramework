using PolarisLite.Web.Components;
using PolarisLite.Web.Events;

namespace PolarisLite.Web;

public class InputFile : WebComponent
{
    public static event EventHandler<ComponentActionEventArgs> Uploaded;
    public virtual bool? IsRequired => string.IsNullOrEmpty(GetAttribute("required")) ? null : bool.Parse(GetAttribute("required"));

    public virtual void Upload(string filePath)
    {
        WrappedElement.SendKeys(filePath);

        Uploaded?.Invoke(this, new ComponentActionEventArgs(this));
    }
}
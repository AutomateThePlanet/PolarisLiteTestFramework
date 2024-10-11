using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;
using PolarisLite.Web.Events;

namespace PolarisLite.Web;

public class Button : WebComponent, IComponentClick, IComponentDisabled, IComponentValue
{
    public static event EventHandler<ComponentActionEventArgs> Clicked;
    
    //private static readonly ThreadLocal<EventHandler<ComponentActionEventArgs>> _clicked = new ThreadLocal<EventHandler<ComponentActionEventArgs>>();

    //public static event EventHandler<ComponentActionEventArgs> Clicked
    //{
    //    add
    //    {
    //        if (!_clicked.IsValueCreated)
    //        {
    //            _clicked.Value = value;
    //        }
    //    }
    //    remove
    //    {
    //        if (_clicked.IsValueCreated)
    //        {
    //            _clicked.Value -= value;
    //        }
    //    }
    //}

    public new bool IsDisabled => base.IsDisabled;

    public new string Value => base.Value;

    public void Click()
    {
        // Use the thread-local event handler
        base.Click(Clicked);
    }
}
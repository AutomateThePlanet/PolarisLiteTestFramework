using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;
using PolarisLite.Web.Events;

namespace PolarisLite.Web;

public class Anchor : WebComponent, IComponentInnerHtml, IComponentHref, IComponentClick
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

    public new string InnerHtml => base.InnerHtml;
    public new string Href => base.Href;

    public void Click()
    {
        base.Click(Clicked);
    }
}

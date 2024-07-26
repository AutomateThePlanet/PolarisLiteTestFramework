using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;
using PolarisLite.Web.Events;

namespace PolarisLite.Web;

public class Select : WebComponent, IComponentDisabled, IComponentClick
{
    public static event EventHandler<ComponentActionEventArgs> Selected;
    public static event EventHandler<ComponentActionEventArgs> Clicked;

    public  WebComponent GetSelected()
    {
        var nativeSelect = new SelectElement(WrappedElement);
        var optionNativeElement = new WebComponent
        {
            FindStrategy = base.FindStrategy,
            WrappedElement = nativeSelect.SelectedOption,
        };
        return optionNativeElement;
    }

    public virtual List<WebComponent> GetAllOptions()
    {
        var nativeSelect = new SelectElement(WrappedElement);
        var options = new List<WebComponent>();
        foreach (var option in nativeSelect.Options)
        {
            var optionNativeElement = new WebComponent
            {
                FindStrategy = this.FindStrategy,
                WrappedElement = option,
            };

            options.Add(optionNativeElement);
        }

        return options;
    }

    public virtual void SelecTFindStrategyText(string text)
    {
        var nativeSelect = new SelectElement(WrappedElement);
        nativeSelect.SelectByText(text);
        WrappedElement = null;

        Selected?.Invoke(this, new ComponentActionEventArgs(this));
    }

    public virtual void SelecTFindStrategyIndex(int index)
    {
        var nativeSelect = new SelectElement(WrappedElement);
        nativeSelect.SelectByIndex(index);
        WrappedElement = null;

        Selected?.Invoke(this, new ComponentActionEventArgs(this));
    }

    public void Click() => base.Click(Clicked);

    public virtual bool IsReadonly => !string.IsNullOrEmpty(GetAttribute("readonly"));

    public virtual bool IsRequired => !string.IsNullOrEmpty(GetAttribute("required"));

    public new bool IsDisabled => base.IsDisabled;
}
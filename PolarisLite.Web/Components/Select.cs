using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public class Select : Component, IComponentDisabled, IComponentClick
{
    public  Component GetSelected()
    {
        var nativeSelect = new SelectElement(WrappedElement);
        var optionNativeElement = new Component
        {
            FindStrategy = base.FindStrategy,
            WrappedElement = nativeSelect.SelectedOption,
        };
        return optionNativeElement;
    }

    public virtual List<Component> GetAllOptions()
    {
        var nativeSelect = new SelectElement(WrappedElement);
        var options = new List<Component>();
        foreach (var option in nativeSelect.Options)
        {
            var optionNativeElement = new Component
            {
                FindStrategy = this.FindStrategy,
                WrappedElement = option,
            };

            options.Add(optionNativeElement);
        }

        return options;
    }

    public virtual void SelectByText(string text)
    {
        var nativeSelect = new SelectElement(WrappedElement);
        nativeSelect.SelectByText(text);
        WrappedElement = null;
    }

    public virtual void SelectByIndex(int index)
    {
        var nativeSelect = new SelectElement(WrappedElement);
        nativeSelect.SelectByIndex(index);
        WrappedElement = null;
    }

    public new void Click() => base.Click();

    public virtual bool IsReadonly => !string.IsNullOrEmpty(GetAttribute("readonly"));

    public virtual bool IsRequired => !string.IsNullOrEmpty(GetAttribute("required"));

    public new bool IsDisabled => base.IsDisabled;
}
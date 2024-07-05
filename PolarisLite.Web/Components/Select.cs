using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public class Select : WebComponent, IComponentDisabled, IComponentClick
{
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
    }

    public virtual void SelecTFindStrategyIndex(int index)
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
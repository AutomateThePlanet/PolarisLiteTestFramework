using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public class Select : ComponentAdapter, IComponentDisabled
{
    public  ComponentAdapter GetSelected()
    {
        var nativeSelect = new SelectElement(WrappedElement);
        var optionNativeElement = new ComponentAdapter
        {
            FindStrategy = base.FindStrategy,
            WrappedElement = nativeSelect.SelectedOption,
        };
        return optionNativeElement;
    }

    public virtual List<ComponentAdapter> GetAllOptions()
    {
        var nativeSelect = new SelectElement(WrappedElement);
        var options = new List<ComponentAdapter>();
        foreach (var option in nativeSelect.Options)
        {
            var optionNativeElement = new ComponentAdapter
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

    public virtual bool IsReadonly => !string.IsNullOrEmpty(GetAttribute("readonly"));

    public virtual bool IsRequired => !string.IsNullOrEmpty(GetAttribute("required"));

    public new bool IsDisabled => base.IsDisabled;
}
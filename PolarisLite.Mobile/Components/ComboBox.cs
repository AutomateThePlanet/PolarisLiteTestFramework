using PolarisLite.Mobile.Contracts;
using System.Diagnostics;

namespace PolarisLite.Mobile.Components;

public class ComboBox : AndroidComponent, IComponentDisabled, IComponentText
{
    public virtual void SelectByText(string value)
    {
        if (WrappedElement.Text != value)
        {
            WrappedElement.Click();
            var innerElementToClick = new TextContainingFindStrategy(value).FindElement(WrappedDriver);
            innerElementToClick.Click();
        }
    }

    public string Text
    {
        get
        {
            var result = base.GetText();
            if (string.IsNullOrEmpty(result))
            {
                var textField = new ClassNameFindStrategy("android.widget.TextView").FindElement(WrappedDriver);
                result = textField.Text;
            }

            return result;
        }
    }

    public new bool IsDisabled => base.IsDisabled();
}
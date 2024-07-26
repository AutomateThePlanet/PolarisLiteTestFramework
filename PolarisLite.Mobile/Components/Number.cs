using PolarisLite.Mobile.Contracts;

namespace PolarisLite.Mobile.Components;

public class Number : AndroidComponent, IComponentDisabled
{
    public virtual void SetNumber(int value)
    {
        TypeText(value.ToString());
    }

    public virtual int GetNumber()
    {
        var resultText = GetText();
        if (string.IsNullOrEmpty(resultText))
        {
            var textField = new ClassNameFindStrategy("android.widget.EditText").FindElement(WrappedDriver);
            resultText = textField.Text;
        }

        int.TryParse(resultText, out var result);

        return result;
    }

    public new bool IsDisabled => base.IsDisabled();
}
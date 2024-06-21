using PolarisLite.Web.Components;

namespace PolarisLite.Web;
public class KendoColorPicker : WebComponent
{
    private readonly string _colorPickerSetColorExpression = "$('#{0}').data('kendoColorPicker').value('#{1}');";

    public void SetColor(string hexValue)
    {
        string scriptToBeExecuted = string.Format(_colorPickerSetColorExpression, FindStrategy.Value, hexValue);
        JavaScriptExecutor.ExecuteScript(scriptToBeExecuted);
    }
}
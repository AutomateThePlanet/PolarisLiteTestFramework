using PolarisLite.Web.Components;

namespace PolarisLite.Web;
public class GaugeNeedle : WebComponent
{
    public void SetValue(int value)
    {
        string scriptToBeExecuted = string.Format("$('#{0}').igRadialGauge('option', 'value', '{1}');", FindStrategy.Value, value);
        JavaScriptService.Execute(scriptToBeExecuted);
    }
}
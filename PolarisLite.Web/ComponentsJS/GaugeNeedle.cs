using PolarisLite.Web.Components;

namespace PolarisLite.Web;
public class GaugeNeedle : ComponentAdapter
{
    public void SetValue(int value)
    {
        string scriptToBeExecuted = string.Format("$('#{0}').igRadialGauge('option', 'value', '{1}');", FindStrategy.Value, value);
        JavaScriptExecutor.ExecuteScript(scriptToBeExecuted);
    }
}
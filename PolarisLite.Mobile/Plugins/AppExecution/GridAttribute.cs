using NUnit.Framework;
using PolarisLite.Mobile.Plugins.AppExecution;
namespace PolarisLite.Mobile.Plugins;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class GridAttribute : LocalExecutionAttribute
{
    public GridAttribute()
    {
        Lifecycle = Lifecycle.RestartEveryTime;
        GridSettings = new GridConfiguration();
        GridSettings.OptionsName = "";
        ExecutionType = ExecutionType.Grid;
    }

    public GridConfiguration GridSettings { get; set; }
}

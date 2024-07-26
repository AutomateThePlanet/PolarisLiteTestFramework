namespace PolarisLite.Mobile.Plugins;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class GridAttribute : LocalExecutionAttribute
{
    public GridAttribute()
    {
        Lifecycle = Lifecycle.RestartEveryTime;
        GridSettings = new GridSettings();
        GridSettings.OptionsName = "";
        ExecutionType = ExecutionType.Grid;
    }

    public GridSettings GridSettings { get; set; }
}

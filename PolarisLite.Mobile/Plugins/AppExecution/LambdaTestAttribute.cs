namespace PolarisLite.Mobile.Plugins;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class LambdaTestAttribute : GridAttribute
{
    public LambdaTestAttribute()
    {
        Lifecycle = Lifecycle.RestartEveryTime;
        GridSettings = new GridSettings();
        GridSettings.OptionsName = "LT:Options";
        ExecutionType = ExecutionType.LambdaTest;
        var userName = Environment.GetEnvironmentVariable("LT_USERNAME", EnvironmentVariableTarget.Machine);
        var accessKey = Environment.GetEnvironmentVariable("LT_ACCESSKEY", EnvironmentVariableTarget.Machine);
        GridSettings.Url = $"https://{userName}:{accessKey}@mobile-hub.lambdatest.com/wd/hub";
        GridSettings.Arguments = new Dictionary<string, object>
        {
            { "isRealMobile", "true" },
            { "build", "1.3" },
            { "video", "true" },
            { "visual", "false" },
            { "w3c", "true" },
            { "autoGrantPermissions", "true" },
            { "project", "POLARIS_ANDROID_RUN" },
            { "appiumVersion", "1.22.0" }
        };
    }
}

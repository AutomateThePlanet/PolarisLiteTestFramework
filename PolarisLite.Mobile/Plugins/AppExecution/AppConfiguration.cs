using PolarisLite.Core;
using System.Runtime.InteropServices;

namespace PolarisLite.Mobile.Plugins;
public class AppConfiguration
{
    public ExecutionType ExecutionType { get; private set; } = ExecutionType.Local;
    public string AppPath { get; private set; }
    public Lifecycle Lifecycle { get; private set; }
    public string DeviceName { get; private set; }
    public string AppPackage { get; private set; }
    public string AppActivity { get; private set; }
    public string AndroidVersion { get; private set; }
    public string TestName { get; set; }
    public bool IsMobileWebExecution { get; private set; }
    public string DefaultBrowser { get; private set; }
    public GridConfiguration GridSettings { get; private set; }
    public Dictionary<string, string> AppiumOptions { get; private set; }

    public AppConfiguration(bool isMobileWebExecution)
    {
        IsMobileWebExecution = isMobileWebExecution;
    }

    public AppConfiguration(Lifecycle lifecycle, ExecutionType executionType, string androidVersion, string deviceName, string appPath, string appPackage, string appActivity)
    {
        ExecutionType = executionType;
        AndroidVersion = androidVersion;
        DeviceName = deviceName;
        AppPath = appPath;
        Lifecycle = lifecycle;
        AppPackage = appPackage;
        AppActivity = appActivity;
        IsMobileWebExecution = false;
        AppiumOptions = new Dictionary<string, string>();
    }

    public static AppConfiguration FromAttribute(LocalExecutionAttribute attribute)
    {
        if (attribute == null)
        {
            return null;
        }

        return new AppConfiguration(
            attribute.Lifecycle,
            attribute.ExecutionType,
            attribute.AndroidVersion,
            attribute.DeviceName,
            attribute.AppPath,
            attribute.AppPackage,
            attribute.AppActivity)
        {
            IsMobileWebExecution = attribute.IsMobileWebTest
        };
    }

    public static AppConfiguration FromAttribute(LambdaTestAttribute attribute)
    {
        if (attribute == null)
        {
            return null;
        }

        var appConfig = new AppConfiguration(
            attribute.Lifecycle,
            attribute.ExecutionType,
            attribute.AndroidVersion,
            attribute.DeviceName,
            attribute.AppPath,
            attribute.AppPackage,
            attribute.AppActivity);

        appConfig.IsMobileWebExecution = attribute.IsMobileWebTest;
        appConfig.GridSettings = attribute.GridSettings;
        return appConfig;
    }

    public override bool Equals(object obj)
    {
        return obj is AppConfiguration config &&
               AppPath == config.AppPath &&
               ExecutionType == config.ExecutionType &&
               EqualityComparer<Lifecycle>.Default.Equals(Lifecycle, config.Lifecycle) &&
               DeviceName == config.DeviceName &&
               AppPackage == config.AppPackage &&
               AppActivity == config.AppActivity &&
               AndroidVersion == config.AndroidVersion &&
               IsMobileWebExecution == config.IsMobileWebExecution;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(AppPath, Lifecycle, DeviceName, AppPackage, AppActivity, AndroidVersion, IsMobileWebExecution, AppiumOptions);
    }
}

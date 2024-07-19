﻿using PolarisLite.Web.Plugins.BrowserExecution;
using PolarisLite.Web.Settings.FilesImplementation;
using System.Runtime.InteropServices;

namespace PolarisLite.Web.Plugins;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class LambdaTestAttribute : GridAttribute
{
    public LambdaTestAttribute(Browser browser = Browser.Chrome, int browserVersion = 0, DesktopWindowSize desktopWindowSize = DesktopWindowSize._1920_1080)
        : base(browser)
    {
        string browserVersionString = browserVersion <= 0 ? "latest" : browserVersion.ToString();
        BrowserConfiguration = new BrowserConfiguration(browser, Lifecycle.RestartEveryTime, ExecutionType.Grid, browserVersionString);
        GridSettings = new GridSettings();
        GridSettings.OptionsName = "LT:Options";
        string userName = Environment.GetEnvironmentVariable("LT_USERNAME", EnvironmentVariableTarget.Machine);
        string accessKey = Environment.GetEnvironmentVariable("LT_ACCESSKEY", EnvironmentVariableTarget.Machine);
        GridSettings.Url = $"https://{userName}:{accessKey}@hub.lambdatest.com/wd/hub";

        string resolution = WindowsSizeResolver.GetWindowSize(desktopWindowSize).ConvertToString();
        GridSettings.Arguments = new Dictionary<string, object>
        {
            { "resolution", "1920x1080" },
            { "platform", "Windows 10" },
            { "visual", "true" },
            { "video", "true" },
            { "build", "1.2" },
            { "project", "POLARIS_RUN" },
            { "selenium_version", "4.21.0" }
        };
    }
}
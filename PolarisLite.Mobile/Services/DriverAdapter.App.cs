using OpenQA.Selenium.Appium.Interfaces;
using PolarisLite.Mobile.Services.Contracts;
using System.Runtime.InteropServices;

namespace PolarisLite.Mobile.Services;

public partial class DriverAdapter : IAppService
{
    public void StartActivity(
        string appPackage,
        string appActivity,
        string appWaitPackage,
        string appWaitActivity,
        bool stopApp)
    {
        try
        {
            _androidDriver.HideKeyboard();
        }
        catch (Exception) { }

        _androidDriver.StartActivity(appPackage, appActivity, appWaitPackage, appWaitActivity, stopApp);
    }

    public void StartActivity(string appPackage, string appActivity)
    {
        _androidDriver.StartActivity(appPackage, appActivity);
    }

    public string GetContext()
    {
        return _androidDriver.Context;
    }

    public void SetContext(string name)
    {
        _androidDriver.Context = name;
    }

    public void BackgroundApp(int seconds)
    {
        _androidDriver.BackgroundApp(TimeSpan.FromSeconds(seconds));
    }

    public void TerminateApp(string appId)
    {
        _androidDriver.TerminateApp(appId);
    }

    public void ActivateApp(string appId)
    {
        _androidDriver.ActivateApp(appId);
    }

    public List<string> GetWebViews()
    {
        var contexts = ((IContextAware)_androidDriver).Contexts;
        return contexts.ToList();
    }

    public void SwitchToDefault()
    {
        var contexts = ((IContextAware)_androidDriver).Contexts;
        var firstContext = contexts.First();
        ((IContextAware)_androidDriver).Context = firstContext;
    }

    public void SwitchToWebView()
    {
        var contexts = ((IContextAware)_androidDriver).Contexts;
        var lastContext = contexts.Last();
        ((IContextAware)_androidDriver).Context = lastContext;
    }

    public void SwitchToWebView(string name)
    {
        var contexts = ((IContextAware)_androidDriver).Contexts;
        var context = contexts.First(c => c.Contains(name));
        ((IContextAware)_androidDriver).Context = context;
    }

    public void SwitchToFirstWebView()
    {
        var contexts = ((IContextAware)_androidDriver).Contexts;
        var firstContext = contexts.First();
        ((IContextAware)_androidDriver).Context = firstContext;
    }

    public void SwitchToWebViewUrlContains(string url)
    {
        SwitchToWebView(() => _androidDriver.Url != null && _androidDriver.Url.Contains(url));
    }

    public void SwitchToWebViewTitleContains(string title)
    {
        SwitchToWebView(() => _androidDriver.Title != null && _androidDriver.Title.Contains(title));
    }

    public void InstallApp(string appPath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            appPath = appPath.Replace('\\', '/');
        }

        _androidDriver.InstallApp(appPath);
    }

    public void RemoveApp(string appId)
    {
        _androidDriver.RemoveApp(appId);
    }

    public bool IsAppInstalled(string bundleId)
    {
        try
        {
            return _androidDriver.IsAppInstalled(bundleId);
        }
        catch (Exception)
        {
            return false;
        }
    }

    private void SwitchToWebView(Func<bool> filterConditionToSwitchWebView)
    {
        var switchedToRightWebView = false;
        var webDriverWait = new WebDriverWait(_androidDriver, TimeSpan.FromSeconds(30))
        {
            PollingInterval = TimeSpan.FromSeconds(1)
        };

        webDriverWait.Until(d =>
        {
            var contexts = ((IContextAware)_androidDriver).Contexts;
            foreach (var context in contexts)
            {
                try
                {
                    ((IContextAware)_androidDriver).Context = context;
                    if (filterConditionToSwitchWebView())
                    {
                        switchedToRightWebView = true;
                        return true;
                    }
                }
                catch (Exception) { }
            }

            if (!switchedToRightWebView)
            {
                _androidDriver.SwitchTo().DefaultContent();
            }

            return switchedToRightWebView;
        });
    }
}

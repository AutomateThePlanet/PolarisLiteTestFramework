namespace PolarisLite.Mobile.Services.Contracts;
public interface IAppService
{
    void ActivateApp(string appId);
    void BackgroundApp(int seconds);
    string GetContext();
    string GetCurrentActivity();
    List<string> GetWebViews();
    void InstallApp(string appPath);
    bool IsAppInstalled(string bundleId);
    void RemoveApp(string appId);
    void SetContext(string name);
    void StartActivity(string appPackage, string appActivity);
    void StartActivity(string appPackage, string appActivity, string appWaitPackage, string appWaitActivity, bool stopApp);
    void SwitchToDefault();
    void SwitchToFirstWebView();
    void SwitchToWebView();
    void SwitchToWebView(string name);
    void SwitchToWebViewTitleContains(string title);
    void SwitchToWebViewUrlContains(string url);
    void TerminateApp(string appId);
    void TerminateApp();
}
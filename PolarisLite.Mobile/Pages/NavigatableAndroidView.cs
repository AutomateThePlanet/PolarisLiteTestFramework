using PolarisLite.Mobile;

namespace PolarisLite.Web;
public abstract class NavigatableAndroidView : AndroidView
{
    public abstract string AppPackage { get; }
    public abstract string AppActivity { get; }

    public void Open()
    {
        App.AppService.StartActivity(AppPackage, AppActivity);
        WaitForViewToLoad();
    }

    public abstract void WaitForViewToLoad();
}

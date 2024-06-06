using PolarisLite.Web.Services;

namespace PolarisLite.Web;
public abstract class NavigatablePage : WebPage
{
    public NavigatablePage(DriverAdapter driver)
        : base(driver)
    {
    }

    public abstract string Url { get; }

    public void Open()
    {
        App.Navigation.GoToUrl(Url);
        WaitForPageToLoad();
    }

    public abstract void WaitForPageToLoad();
}

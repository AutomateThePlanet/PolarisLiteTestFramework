using PolarisLite.Web.Events;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter : INavigationService
{
    public static event EventHandler<UrlNavigatedEventArgs> UrlNavigatedEvent;

    public string Url => _webDriver.Url;

    public void GoToUrl(string url)
    {
        _webDriver.Navigate().GoToUrl(url);
        UrlNavigatedEvent?.Invoke(this, new UrlNavigatedEventArgs(url));
    }
}

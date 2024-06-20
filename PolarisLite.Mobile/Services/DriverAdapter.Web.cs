using PolarisLite.Web;

namespace PolarisLite.Mobile.Services;

public partial class DriverAdapter
{
    public Web.IElementFindService Elements => _webDriverAdapter;
    public IBrowserService Browser => _webDriverAdapter;
    public INavigationService Navigation => _webDriverAdapter;
    public ICookiesService Cookies => _webDriverAdapter;
    public IDialogService Dialog => _webDriverAdapter;
    public IInteractionsService InteractionsService => _webDriverAdapter;
    public IJavaScriptService JavaScriptService => _webDriverAdapter;
}

using PolarisLite.Mobile.Components;
using PolarisLite.Web;

namespace PolarisLite.Mobile;

public interface IWebService
{
    Web.IElementFindService Elements { get; }
    IBrowserService Browser { get; }
    INavigationService Navigation { get; }
    ICookiesService Cookies { get; }
    IInteractionsService Interactions { get; }
    IJavaScriptService JavaScript { get; }
}

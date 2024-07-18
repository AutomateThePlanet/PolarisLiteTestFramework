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

    public TPage Create<TPage>()
       where TPage : WebPage, new();

    public TPage GoTo<TPage>()
       where TPage : NavigatablePage, new();
}

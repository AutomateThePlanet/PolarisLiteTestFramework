﻿using PolarisLite.Web.Plugins.BrowserExecution;
using PolarisLite.Web.Services;

namespace PolarisLite.Web;
public class App
{
    private readonly DriverAdapter _driver;

    public App()
    {
        _driver = new DriverAdapter();
    }

    public IElementFindService Elements => _driver;
    public IBrowserService Browser => _driver;
    public INavigationService Navigation => _driver;
    public ICookiesService Cookies => _driver;
    public IDialogService Dialog => _driver;
    public IInteractionsService InteractionsService => _driver;
    public IJavaScriptService JavaScriptService => _driver;

    public void AddBrowserOptions(string option, string value)
    {
        DriverFactory.CustomDriverOptions.Add(option, value);
    }

    public TPage Create<TPage>()
        where TPage : WebPage, new()
    {
        TPage pageInstance = new TPage();
        return pageInstance;
    }

    public TPage GoTo<TPage>()
       where TPage : NavigatablePage, new()
    {
        TPage pageInstance = new TPage();
        pageInstance.Open();
        return pageInstance;
    }
}

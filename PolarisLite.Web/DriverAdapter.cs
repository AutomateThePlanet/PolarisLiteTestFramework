using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using PolarisLite.Core.Infrastructure;
using PolarisLite.Locators;
using WebDriverManager.DriverConfigs.Impl;

namespace PolarisLite.Web;

public class DriverAdapter
{
    private IWebDriver _webDriver;
    private WebDriverWait _webDriverWait;
    private NativeElementFindService _nativeElementFindService;

    public string Url => _webDriver.Url;

    public void Start(Browser browser)
    {
        switch (browser)
        {
            case Browser.Chrome:
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                _webDriver = new ChromeDriver();
                break;
            case Browser.Firefox:
                new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                _webDriver = new FirefoxDriver();
                break;
            case Browser.Edge:
                new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                _webDriver = new EdgeDriver();
                break;
            case Browser.Safari:
                _webDriver = new SafariDriver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
        }

        _webDriver.Manage().Window.Maximize();
        _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
        _nativeElementFindService = new NativeElementFindService(_webDriver, _webDriver);
    }

    public void Quit()
    {
        _webDriver.Quit();
    }

    public void GoToUrl(string url)
    {
        _webDriver.Navigate().GoToUrl(url);
    }

    public TComponent FindComponent<TComponent>(FindStrategy findStrategy)
        where TComponent : ComponentAdapter
    {
        IWebElement nativeWebElement =
            _nativeElementFindService.Find(findStrategy);
        var component = InstanceFactory.Create<TComponent>();
        component.FindStrategy = findStrategy;
        component.WrappedDriver = _webDriver;
        component.WrappedElement = nativeWebElement;
        return component;
    }

    public List<TComponent> FindComponents<TComponent>(FindStrategy findStrategy)
        where TComponent : ComponentAdapter
    {
        IEnumerable<IWebElement> nativeWebElements =
            _nativeElementFindService.FindAll(findStrategy);
        var components = new List<TComponent>();
        foreach (var nativeWebElement in nativeWebElements)
        {
            var component = InstanceFactory.Create<TComponent>();
            component.FindStrategy = findStrategy;
            component.WrappedDriver = _webDriver;
            component.WrappedElement = nativeWebElement;
            components.Add(component);
        }

        return components;
    }
    public void Refresh()
    {
        _webDriver.Navigate().Refresh();
    }

    public void DeleteAllCookies()
    {
        _webDriver.Manage().Cookies.DeleteAllCookies();
    }

    public void ExecuteScript(string script, params object[] args)
    {
        ((IJavaScriptExecutor)_webDriver).ExecuteScript(script, args);
    }

    public void WaitForAjax()
    {
        _webDriverWait.Until(driver =>
        {
            var script = "return window.jQuery != undefined && jQuery.active == 0";
            return (bool)((IJavaScriptExecutor)driver).ExecuteScript(script);
        });
    }
}

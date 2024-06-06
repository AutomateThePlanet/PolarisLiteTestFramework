using PolarisLite.Core.Infrastructure;
using PolarisLite.Locators;
using PolarisLite.Web.Plugins.BrowserExecution;

namespace PolarisLite.Web;

public class DriverAdapter
{
    private IWebDriver _webDriver;
    private WebDriverWait _webDriverWait;
    private NativeElementFindService _nativeElementFindService;
    private List<WaitStrategy> _waitStrategies;

    public DriverAdapter()
    {
        _webDriver = DriverFactory.WrappedDriver;
        _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
        _nativeElementFindService = new NativeElementFindService(_webDriver, _webDriver);
        _waitStrategies = new List<WaitStrategy>();
        _waitStrategies.Add(new ToExistsWaitStrategy());
    }

    public string Url => _webDriver.Url;

    //public void Start(Browser browser)
    //{
    //    switch (browser)
    //    {
    //        case Browser.Chrome:
    //            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
    //            _webDriver = new ChromeDriver();
    //            break;
    //        case Browser.Firefox:
    //            new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
    //            _webDriver = new FirefoxDriver();
    //            break;
    //        case Browser.Edge:
    //            new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
    //            _webDriver = new EdgeDriver();
    //            break;
    //        case Browser.Safari:
    //            _webDriver = new SafariDriver();
    //            break;
    //        default:
    //            throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
    //    }

    //    _webDriver.Manage().Window.Maximize();
    //    _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
    //    _nativeElementFindService = new NativeElementFindService(_webDriver, _webDriver);
    //}

    public void Quit()
    {
        _webDriver.Quit();
    }

    public void GoToUrl(string url)
    {
        _webDriver.Navigate().GoToUrl(url);
    }

    public TComponent FindById<TComponent>(string id)
       where TComponent : ComponentAdapter
    {
        return FindComponent<TComponent>(new IdFindStrategy(id));
    }

    public TComponent FindByIdContaining<TComponent>(string id)
       where TComponent : ComponentAdapter
    {
        return FindComponent<TComponent>(new IdContainingFindStrategy(id));
    }

    public TComponent FindByXPath<TComponent>(string xpath)
      where TComponent : ComponentAdapter
    {
        return FindComponent<TComponent>(new XPathFindStrategy(xpath));
    }

    public TComponent FindByCss<TComponent>(string css)
        where TComponent : ComponentAdapter
    {
        return FindComponent<TComponent>(new CssFindStrategy(css));
    }

    public TComponent FindByClass<TComponent>(string value)
       where TComponent : ComponentAdapter
    {
        return FindComponent<TComponent>(new ClassFindStrategy(value));
    }

    public List<TComponent> FindComponentsById<TComponent>(string id)
      where TComponent : ComponentAdapter
    {
        return FindComponents<TComponent>(new IdFindStrategy(id));
    }

    public List<TComponent> FindComponentsByXPath<TComponent>(string xpath)
     where TComponent : ComponentAdapter
    {
        return FindComponents<TComponent>(new XPathFindStrategy(xpath));
    }

    public List<TComponent> FindComponentsByCss<TComponent>(string css)
    where TComponent : ComponentAdapter
    {
        return FindComponents<TComponent>(new CssFindStrategy(css));
    }

    public TComponent FindComponent<TComponent>(FindStrategy findStrategy)
        where TComponent : ComponentAdapter
    {
        foreach (var waitStrategy in _waitStrategies)
        {
            waitStrategy.WaitUntil(_webDriver, _webDriver, findStrategy.Convert());
        }

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
        foreach (var waitStrategy in _waitStrategies)
        {
            waitStrategy.WaitUntil(_webDriver, _webDriver, findStrategy.Convert());
        }

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

    public void EnsureState(WaitStrategy waitStrategy)
    {
        _waitStrategies.Add(waitStrategy);
    }
}

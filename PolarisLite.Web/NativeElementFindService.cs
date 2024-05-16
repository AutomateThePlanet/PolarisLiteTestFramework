﻿using PolarisLite.Locators;

namespace PolarisLite.Web;

public class NativeElementFindService
{
    private readonly ISearchContext _searchContext;
    private readonly IWebDriver _driver;

    public NativeElementFindService(ISearchContext searchContext, IWebDriver driver)
    {
        _searchContext = searchContext;
        _driver = driver;
    }

    public IWebElement Find(FindStrategy findStrategy)
    {
        Wait.To.Exists().WaitUntil(_searchContext, _driver, findStrategy.Convert());
        var element = _searchContext.FindElement(findStrategy.Convert());
        return element;
    }

    public IEnumerable<IWebElement> FindAll(FindStrategy findStrategy)
    {
        Wait.To.Exists().WaitUntil(_searchContext, _driver, findStrategy.Convert());
        IEnumerable<IWebElement> result = _searchContext.FindElements(findStrategy.Convert());
        return result;
    }
}

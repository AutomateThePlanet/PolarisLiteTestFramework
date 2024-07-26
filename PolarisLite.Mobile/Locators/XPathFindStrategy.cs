namespace PolarisLite.Mobile;

public class XPathFindStrategy : FindStrategy
{
    public XPathFindStrategy(string name)
        : base(name)
    {
    }

    public override AppiumElement FindElement(AndroidDriver searchContext)
    {
        return searchContext.FindElement(MobileBy.XPath(Value));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AndroidDriver searchContext)
    {
        return searchContext.FindElements(MobileBy.XPath(Value));
    }

    public override AppiumElement FindElement(AppiumElement element)
    {
        return element.FindElement(MobileBy.XPath(Value));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AppiumElement element)
    {
        return element.FindElements(MobileBy.XPath(Value));
    }

    public override string ToString()
    {
        return $"ByXPath = {Value}";
    }
}

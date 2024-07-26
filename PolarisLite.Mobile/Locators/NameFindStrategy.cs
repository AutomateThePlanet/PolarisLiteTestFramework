namespace PolarisLite.Mobile;

public class NameFindStrategy : FindStrategy
{
    public NameFindStrategy(string name)
        : base(name)
    {
    }

    public override AppiumElement FindElement(AndroidDriver searchContext)
    {
        return searchContext.FindElement(MobileBy.Name(Value));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AndroidDriver searchContext)
    {
        return searchContext.FindElements(MobileBy.Name(Value));
    }

    public override AppiumElement FindElement(AppiumElement element)
    {
        return element.FindElement(MobileBy.Name(Value));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AppiumElement element)
    {
        return element.FindElements(MobileBy.Name(Value));
    }

    public override string ToString()
    {
        return $"Name = {Value}";
    }
}

namespace PolarisLite.Mobile;

public class TagNameFindStrategy : FindStrategy
{
    public TagNameFindStrategy(string name)
        : base(name)
    {
    }

    public override AppiumElement FindElement(AndroidDriver searchContext)
    {
        return searchContext.FindElement(MobileBy.TagName(Value));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AndroidDriver searchContext)
    {
        return searchContext.FindElements(MobileBy.TagName(Value));
    }

    public override AppiumElement FindElement(AppiumElement element)
    {
        return element.FindElement(MobileBy.TagName(Value));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AppiumElement element)
    {
        return element.FindElements(MobileBy.TagName(Value));
    }

    public override string ToString()
    {
        return $"TagName = {Value}";
    }
}

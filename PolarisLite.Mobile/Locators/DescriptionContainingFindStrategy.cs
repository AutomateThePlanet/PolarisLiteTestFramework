
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace PolarisLite.Mobile;

public class DescriptionContainingFindStrategy : FindStrategy
{
    private readonly string _locatorValue;

    public DescriptionContainingFindStrategy(string name)
        : base(name)
    {
        _locatorValue = $"new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().descriptionContains(\"{Value}\"));";
    }

    public override AppiumElement FindElement(AndroidDriver searchContext)
    {
        return searchContext.FindElement(MobileBy.AndroidUIAutomator(_locatorValue));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AndroidDriver searchContext)
    {
        return searchContext.FindElements(MobileBy.AndroidUIAutomator(_locatorValue));
    }

    public override AppiumElement FindElement(AppiumElement element)
    {
        return element.FindElement(MobileBy.AndroidUIAutomator(_locatorValue));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AppiumElement element)
    {
        return element.FindElements(MobileBy.AndroidUIAutomator(_locatorValue));
    }

    public override string ToString()
    {
        return $"Description contains {Value}";
    }
}

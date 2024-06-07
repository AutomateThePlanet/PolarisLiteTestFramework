using System.Collections.Generic;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace PolarisLite.Mobile;

public class TextContainingFindStrategy : FindStrategy
{
    private readonly string _locatorValue;

    public TextContainingFindStrategy(string name)
        : base(name)
    {
        _locatorValue = $"new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().textContains(\"{Value}\"));";
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
        return $"Text = {Value}";
    }
}

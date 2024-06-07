using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace PolarisLite.Mobile;

public class AndroidUIAutomatorFindStrategy : FindStrategy
{
    public AndroidUIAutomatorFindStrategy(string name)
        : base(name)
    {
    }

    public override AppiumElement FindElement(AndroidDriver searchContext)
    {
        return searchContext.FindElement(MobileBy.AndroidUIAutomator(Value));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AndroidDriver searchContext)
    {
        return searchContext.FindElements(MobileBy.AndroidUIAutomator(Value));
    }

    public override AppiumElement FindElement(AppiumElement element)
    {
        return element.FindElement(MobileBy.AndroidUIAutomator(Value));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AppiumElement element)
    {
        return element.FindElements(MobileBy.AndroidUIAutomator(Value));
    }

    public override string ToString()
    {
        return $"AndroidUIAutomator = {Value}";
    }
}

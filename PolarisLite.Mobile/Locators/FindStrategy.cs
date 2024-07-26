namespace PolarisLite.Mobile;

public abstract class FindStrategy
{
    public FindStrategy(string name)
    {
        Value = name;
    }

    public string Value { get; }

    public abstract AppiumElement FindElement(AndroidDriver driver);

    public abstract IEnumerable<AppiumElement> FindAllElements(AndroidDriver driver);

    public abstract AppiumElement FindElement(AppiumElement element);

    public abstract IEnumerable<AppiumElement> FindAllElements(AppiumElement element);
}

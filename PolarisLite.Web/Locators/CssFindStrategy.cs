namespace PolarisLite.Locators;

public class CssFindStrategy : FindStrategy
{
    public CssFindStrategy(string value)
        : base(value)
    {
    }

    public override By Convert() => By.CssSelector(Value);
}

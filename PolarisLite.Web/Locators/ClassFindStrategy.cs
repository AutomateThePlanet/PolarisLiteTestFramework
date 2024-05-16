namespace PolarisLite.Locators;

public class ClassFindStrategy : FindStrategy
{
    public ClassFindStrategy(string value)
        : base(value)
    {
    }

    public override By Convert() => By.XPath($"//*[@class='{Value}']");
}

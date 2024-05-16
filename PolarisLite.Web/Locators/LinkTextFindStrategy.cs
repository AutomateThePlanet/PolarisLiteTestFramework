namespace PolarisLite.Locators;

public class LinkTextFindStrategy : FindStrategy
{
    public LinkTextFindStrategy(string value)
        : base(value)
    {
    }

    public override By Convert() => By.LinkText(Value);
}

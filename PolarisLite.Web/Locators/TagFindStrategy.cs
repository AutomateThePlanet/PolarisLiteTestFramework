namespace PolarisLite.Locators;

public class TagFindStrategy : FindStrategy
{
    public TagFindStrategy(string value)
        : base(value)
    {
    }

    public override By Convert() => By.TagName(Value);
}

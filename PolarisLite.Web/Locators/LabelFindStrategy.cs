namespace PolarisLite.Locators;

public class LabelFindStrategy : FindStrategy
{
    public LabelFindStrategy(string label)
        : base(label)
    {
    }

    public override By Convert()
    {
        return By.XPath($"//label[text()='{Value}']/following-sibling::div//input[@type='text']");
    }
}

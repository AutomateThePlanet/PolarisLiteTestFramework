namespace PolarisLite.Mobile.Components;

public class FindStrategyFactory
{
    public IdFindStrategy Id(string id) => new IdFindStrategy(id);

    public IdContainingFindStrategy IdContaining(string id) => new IdContainingFindStrategy(id);

    public DescriptionFindStrategy Description(string description) => new DescriptionFindStrategy(description);

    public DescriptionContainingFindStrategy DescriptionContaining(string description) => new DescriptionContainingFindStrategy(description);

    public TextFindStrategy Text(string text) => new TextFindStrategy(text);

    public TextContainingFindStrategy TextContaining(string text) => new TextContainingFindStrategy(text);

    public ClassNameFindStrategy ClassName(string className) => new ClassNameFindStrategy(className);

    public NameFindStrategy Name(string name) => new NameFindStrategy(name);

    public TagNameFindStrategy TagName(string tag) => new TagNameFindStrategy(tag);

    public XPathFindStrategy XPath(string name) => new XPathFindStrategy(name);

    public AndroidUIAutomatorFindStrategy AndroidUIAutomator(string name) => new AndroidUIAutomatorFindStrategy(name);
}

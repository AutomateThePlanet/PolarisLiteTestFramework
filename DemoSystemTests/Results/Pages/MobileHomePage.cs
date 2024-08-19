using PolarisLite.Web;

namespace DemoSystemTests.Web.Results;

// NOTE: Move common logic to base class for home page and mark fields as abstract so that
// we can implmenent the right locator for each version.
public class MobileHomePage : WebPage
{
    public TextField SearchInput => App.Elements.FindAllByXPath<TextField>("//input[@name='search']").Last();
    public Button SearchButton => App.Elements.FindByXPath<Button>("//button[@title='Search']");

    public void SearchProduct(string searchText)
    {
        SearchInput.TypeText(searchText);
    }
}

using PolarisLite.Web;

namespace DemoSystemTests.Framework.Web.Pages;
public class HomePage : WebPage
{
    public TextField SearchInput => App.Elements.FindByXPath<TextField>("//input[@name='search']");

    public void SearchProduct(string searchText)
    {
        SearchInput.TypeText(searchText);
    }
}

using PolarisLite.Web;

namespace DemoSystemTests.Web.Results;
public class HomePage : WebPage
{
    public TextField SearchInput => App.Elements.FindAllByXPath<TextField>("//input[@name='search']").First();
    public Button SearchButton => App.Elements.FindByXPath<Button>("//button[@title='Search' or text()='Search']");

    public void SearchProduct(string searchText)
    {
        SearchInput.TypeText(searchText);
    }
}

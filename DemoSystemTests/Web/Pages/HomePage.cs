using PolarisLite.Web;

namespace DemoSystemTests.Web;
public class HomePage : WebPage
{
    public TextField SearchInput => App.Elements.FindAllByXPath<TextField>("//input[@name='search']").Last();
    public Button SearchButton => App.Elements.FindByXPath<Button>("//button[@title='Search']");

    public void SearchProduct(string searchText)
    {
        SearchInput.TypeText(searchText);
    }
}

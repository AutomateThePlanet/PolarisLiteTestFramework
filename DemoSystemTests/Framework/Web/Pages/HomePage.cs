using PolarisLite.Web;
using PolarisLite.Web.Assertions;

namespace DemoSystemTests.Framework.Web.Pages;
public class HomePage : NavigatablePage
{
    public TextField SearchInput => App.Elements.FindByXPath<TextField>("//input[@name='search']");

    public override string Url => "https://ecommerce-playground.lambdatest.io/";

    public void SearchProduct(string searchText)
    {
        SearchInput.TypeText(searchText);
    }

    public override void WaitForPageToLoad()
    {
        SearchInput.ValidateIsVisible();
    }
}

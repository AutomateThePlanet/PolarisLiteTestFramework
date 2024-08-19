using PolarisLite.Web;

namespace DemoSystemTests.Web.Results;
public class SearchProductPage : WebPage
{
    public TextField SearchInput => App.Elements.FindById<TextField>("input-search");
    public Button SearchButton => App.Elements.FindById<Button>("button-search");
    public Select SearchCategoriesSelect => App.Elements.FindByXPath<Select>("//select[@name='category_id']");
    public CheckBox SearchProductDescriptionsCheckBox => App.Elements.FindByXPath<CheckBox>("//label[text()='Search in product descriptions']");
    public Button ProductCompareButton => App.Elements.FindByXPath<Button>("//a[text() = 'Product Compare (0)']");
    public Button FilterButton => App.Elements.FindByXPath<Button>("//a[normalize-space(text()) = 'Filter']");
    public Button ListViewButton => App.Elements.FindById<Button>("list-view");
    public Button GridViewButton => App.Elements.FindById<Button>("grid-view");
    public Select InputLimitSelect => App.Elements.FindByXPath<Select>("//select[starts-with(@id, 'input-limit')]");
    public Select SortBySelect => App.Elements.FindByXPath<Select>("//select[starts-with(@id, 'input-sort')]");

    public void OpenItem(int productId)
    {
        var itemLinkXpath = $"//a[contains(@href, 'product_id={productId}')]";
        var itemLink = App.Elements.FindByXPath<Button>(itemLinkXpath);
        itemLink.Click();
    }
}

using DemoSystemTests.Framework.Web.Pages.Models;
using PolarisLite.Web;

namespace DemoSystemTests.Framework.Web.Pages;

public class ProductPage : WebPage
{
    public Button AddToCartButton => App.Elements.FindAllByXPath<Button>("//button[@title='Add to Cart']").Last();
    public Button CompareButton => App.Elements.FindByXPath<Button>("//a[@aria-label='Compare']");
    public TextField QuantityField => App.Elements.FindAllByXPath<TextField>("//input[@name='quantity']").Last();
    public List<Button> CompareProductButtons => App.Elements.FindAllByXPath<Button>("//button[@title='Compare this Product']");

    public void SelectProductFromAutocomplete(int productId)
    {
        var autocompleteItemXPath = $"//ul[contains(@class, 'dropdown-menu autocomplete')]/li/div/h4/a[contains(@href, 'product_id={productId}')]";
        var autocompleteItem = App.Elements.FindByXPath<Button>(autocompleteItemXPath);
        autocompleteItem.Click();
    }

    public void AddToCart(string quantity)
    {
        QuantityField.TypeText(quantity);
        AddToCartButton.Click();
    }

    public void CompareLastProduct()
    {
        CompareProductButtons.Last().Click();
    }

    public void GoToComparePage()
    {
        CompareButton.Click();
    }

    public void AssertCompareProductDetails(ProductDetails expectedProduct, int index)
    {
        var productName = App.Elements.FindByXPath<Label>(GetCompareProductDetailsCellXPath("Product", index));
        Assert.That(productName.Text, Is.EqualTo(expectedProduct.Name));
        // Add more assertions for Price, Model, Brand, Weight, etc.
    }

    private string GetCompareProductDetailsCellXPath(string cellName, int productCompareIndex)
    {
        return $"//table/tbody/tr/td[text()='{cellName}']/following-sibling::td[{productCompareIndex}]";
    }
}

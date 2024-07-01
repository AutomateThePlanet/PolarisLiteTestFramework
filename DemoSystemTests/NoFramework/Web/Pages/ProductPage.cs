namespace DemoSystemTests.NoFramework;

public class ProductPage : WebPage
{
    public ProductPage(IWebDriver driver, WebDriverWait wait, Actions actions) 
        : base(driver, wait, actions)
    {
    }

    public IWebElement AddToCartButton => _driver.FindElements(By.XPath("//button[@title='Add to Cart']")).Last();
    public IWebElement CompareButton => _driver.FindElement(By.XPath("//a[@aria-label='Compare']"));
    public IWebElement QuantityField => _driver.FindElements(By.XPath("//input[@name='quantity']")).Last();
    public IReadOnlyCollection<IWebElement> CompareProductButtons => _driver.FindElements(By.XPath("//button[@title='Compare this Product']"));

    public void SelectProductFromAutocomplete(int productId)
    {
        var autocompleteItemXPath = $"//ul[contains(@class, 'dropdown-menu autocomplete')]/li/div/h4/a[contains(@href, 'product_id={productId}')]";
        var autocompleteItem = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(autocompleteItemXPath)));
        autocompleteItem.Click();
    }

    public void AddToCart(string quantity)
    {
        QuantityField.Clear();
        QuantityField.SendKeys(quantity);
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
        var productName = _wait.Until(ExpectedConditions.ElementExists(By.XPath(GetCompareProductDetailsCellXPath("Product", index))));
        Assert.That(productName.Text, Is.EqualTo(expectedProduct.Name));
        // Add more assertions for Price, Model, Brand, Weight, etc.
    }

    private string GetCompareProductDetailsCellXPath(string cellName, int productCompareIndex)
    {
        return $"//table/tbody/tr/td[text()='{cellName}']/following-sibling::td[{productCompareIndex}]";
    }
}

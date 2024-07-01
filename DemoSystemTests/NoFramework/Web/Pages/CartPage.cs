namespace DemoSystemTests.NoFramework;
public class CartPage : WebPage
{
    public CartPage(IWebDriver driver, WebDriverWait wait, Actions actions) 
        : base(driver, wait, actions)
    {
    }

    public IWebElement ViewCartButton => _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[normalize-space(.)='View Cart']")));
    public IWebElement CheckoutButton => _driver.FindElements(By.XPath("//a[normalize-space(.)='Checkout']")).Last();
    public List<IWebElement> CartItems => _driver.FindElements(By.CssSelector("div.cart-item")).ToList();
    public IWebElement TotalPrice => _driver.FindElements(By.XPath("//td[text()='Total:']/following-sibling::td/strong")).Last();

    public void ViewCart()
    {
        ViewCartButton.Click();
    }

    public void Checkout()
    {
        CheckoutButton.Click();
    }

    public void UpdateQuantity(int itemIndex, int quantity)
    {
        var quantityField = CartItems[itemIndex].FindElement(By.XPath(".//input[@type='number']"));
        quantityField.Clear();
        quantityField.SendKeys(quantity.ToString());
    }

    public void RemoveItem(int itemIndex)
    {
        var removeButton = CartItems[itemIndex].FindElement(By.XPath(".//button[@title='Remove']"));
        removeButton.Click();
    }

    public void AssertTotalPrice(string expectedPrice)
    {
        Assert.That(TotalPrice.Text, Is.EqualTo(expectedPrice));
    }
}

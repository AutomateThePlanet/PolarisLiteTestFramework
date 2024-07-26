namespace DemoSystemTests.NoFramework;
public class WebSite
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;
    private readonly Actions _actions;

    public WebSite(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        _actions = new Actions(driver);

        HomePage = new HomePage(_driver, _wait, _actions);
        ProductPage = new ProductPage(_driver, _wait, _actions);
        CartPage = new CartPage(_driver, _wait, _actions);
        CheckoutPage = new CheckoutPage(_driver, _wait, _actions);
    }

    public HomePage HomePage { get; private set; }
    public ProductPage ProductPage { get; private set; }
    public CartPage CartPage { get; private set; }
    public CheckoutPage CheckoutPage { get; private set; }
}

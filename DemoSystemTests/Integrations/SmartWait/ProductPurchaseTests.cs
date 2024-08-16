using DemoSystemTests.Framework.Web.Pages;
using DemoSystemTests.Framework.Web.Pages.Models;
using PolarisLite.Core.Layout.Second;
using PolarisLite.Web;
using PolarisLite.Web.Configuration.StaticImplementation;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Integrations.SmartWait;

[TestFixture]
[LambdaTest(BrowserType.Chrome, enableAutoHealing: true, smartWait: 30)]
public class ProductPurchaseTests : WebTest
{
    public HomePage HomePage { get; private set; }
    public MobileHomePage MobileHomePage { get; private set; }
    public ProductPage ProductPage { get; private set; }
    public CartPage CartPage { get; private set; }
    public CheckoutPage CheckoutPage { get; private set; }
    public SearchProductPage SearchProductPage { get; private set; }

    protected override void TestInitialize()
    {
        WebSettings.TimeoutSettings.WaitForAjaxTimeout = 0;
        WebSettings.TimeoutSettings.PageLoadTimeout = 0;
        WebSettings.TimeoutSettings.ScriptTimeout = 0;
        WebSettings.TimeoutSettings.ValidationsTimeout = 0;
        WebSettings.TimeoutSettings.ElementToBeVisibleTimeout = 0;
        WebSettings.TimeoutSettings.ElementToExistTimeout = 0;
        WebSettings.TimeoutSettings.ElementToBeClickableTimeout = 0;

        HomePage = App.Create<HomePage>();
        MobileHomePage = App.Create<MobileHomePage>();
        ProductPage = App.Create<ProductPage>();
        CartPage = App.Create<CartPage>();
        CheckoutPage = App.Create<CheckoutPage>();
        SearchProductPage = App.Create<SearchProductPage>();
        App.Navigation.GoToUrl("https://ecommerce-playground.lambdatest.io/");
    }

    [Test]
    public void PurchaseTwoSameProduct()
    {
        var expectedProduct1 = new ProductDetails
        {
            Name = "iPod Touch",
            Id = 59,
            Price = "$194.00",
            Model = "Product 5",
            Brand = "Apple",
            Weight = "5.00kg",
            Quantity = "2"
        };

        var userDetails = new UserDetails
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            Telephone = "1234567890",
            Password = "password123",
            ConfirmPassword = "password123",
            AccountType = AccountOption.Register
        };

        var billingAddress = new BillingAddress
        {
            FirstName = "John",
            LastName = "Doe",
            Company = "Acme Corp",
            Address1 = "123 Main St",
            Address2 = "Apt 4",
            City = "Metropolis",
            PostCode = "12345",
            Country = "United States",
            Region = "Alabama"
        };

        HomePage.SearchProduct("iPod Tou");
        ProductPage.SelectProductFromAutocomplete(expectedProduct1.Id);
        ProductPage.AddToCart(expectedProduct1.Quantity);
        CartPage.ViewCart();
        CartPage.AssertTotalPrice("$388.00");

        CartPage.Checkout();
        CheckoutPage.FillUserDetails(userDetails);
        CheckoutPage.FillBillingAddress(billingAddress);

        CheckoutPage.AgreeToTerms();
        CheckoutPage.CompleteCheckout();
    }
}
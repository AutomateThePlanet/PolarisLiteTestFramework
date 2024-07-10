using DemoSystemTests.Framework.Web.Pages;
using DemoSystemTests.Framework.Web.Pages.Models;
using PolarisLite.Web;
using PolarisLite.Web.Core;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Framework.Web;

[TestFixture]
[LocalExecution(Browser.Chrome, Lifecycle.ReuseIfStarted)]
public class ProductPurchaseTests : WebTest
{
    public HomePage HomePage { get; private set; }
    public ProductPage ProductPage { get; private set; }
    public CartPage CartPage { get; private set; }
    public CheckoutPage CheckoutPage { get; private set; }
    public App App => new App();

    [SetUp]
    public void SetUp()
    {
        //DriverFactory.Start(new BrowserConfiguration());
        HomePage = App.Create<HomePage>();
        ProductPage = App.Create<ProductPage>();
        CartPage = App.Create<CartPage>();
        CheckoutPage = App.Create<CheckoutPage>();

        HomePage.Open();
        //App.Navigation.GoToUrl("https://ecommerce-playground.lambdatest.io/");
    }

    [TearDown]
    public void TearDown()
    {
        DriverFactory.Dispose();
    }

    [Test]
    public void CorrectInformationDisplayedInCompareScreen_WhenOpenProductFromSearchResults_TwoProducts()
    {
        // Arrange
        var expectedProduct1 = new ProductDetails
        {
            Name = "iPod Touch",
            Id = 59,
            Price = "$194.00",
            Model = "Product 5",
            Brand = "Apple",
            Weight = "5.00kg"
        };

        var expectedProduct2 = new ProductDetails
        {
            Name = "iPod Shuffle",
            Id = 58,
            Price = "$182.00",
            Model = "Product 7",
            Brand = "Apple",
            Weight = "5.00kg"
        };

        HomePage.SearchProduct("iPod Tou");
        ProductPage.SelectProductFromAutocomplete(expectedProduct1.Id);
        ProductPage.CompareLastProduct();
        HomePage.SearchProduct("iPod Shuff");
        ProductPage.SelectProductFromAutocomplete(expectedProduct2.Id);
        ProductPage.CompareLastProduct();

        ProductPage.GoToComparePage();

        ProductPage.AssertCompareProductDetails(expectedProduct1, 1);
        ProductPage.AssertCompareProductDetails(expectedProduct2, 2);
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
        CheckoutPage.AssertTotalPrice("$396.00");

        CheckoutPage.AgreeToTerms();
        CheckoutPage.CompleteCheckout();
    }
}

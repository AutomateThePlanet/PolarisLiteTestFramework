using DemoSystemTests.Framework.Web.Pages;
using PolarisLite.Mobile;
using PolarisLite.Mobile.Core.NUnit;
using PolarisLite.Mobile.Plugins;
using PolarisLite.Web;
using PolarisLite.Core.Layout.Second;
using DemoSystemTests.Framework.Web.Pages.Models;

namespace DemoSystemTests.Mobile.Framework;

//[LocalExecution(AndroidVersion = "13.0",
//    DeviceName = "pixel5-test-device-13-new",
//    IsMobileWebTest = true,
//    BrowserName = "Chrome",
//    Lifecycle = Lifecycle.RestartEveryTime)]
[LambdaTest(AndroidVersion = "13",
    DeviceName = "Pixel 6",
    IsMobileWebTest = true,
    Lifecycle = Lifecycle.RestartEveryTime)]
public class WorkingWithMobileWebTests : AndroidTest
{
    public MobileHomePage MobileHomePage { get; private set; }
    public ProductPage ProductPage { get; private set; }
    public CartPage CartPage { get; private set; }
    public CheckoutPage CheckoutPage { get; private set; }
    public SearchProductPage SearchProductPage { get; private set; }

    protected override void TestInitialize()
    {
        MobileHomePage = App.Web.Create<MobileHomePage>();
        ProductPage = App.Web.Create<ProductPage>();
        CartPage = App.Web.Create<CartPage>();
        CheckoutPage = App.Web.Create<CheckoutPage>();
        SearchProductPage = App.Web.Create<SearchProductPage>();
        App.Web.Navigation.GoToUrl("https://ecommerce-playground.lambdatest.io/");
    }

    [Test]
    public void PurchaseTwoSameProduct_WhenSearchingWithoutAutocomplete()
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

        MobileHomePage.SearchProduct("iPod Tou");
        MobileHomePage.SearchButton.Click();
        SearchProductPage.OpenItem(expectedProduct1.Id);
        //ProductPage.SelectProductFromAutocomplete(expectedProduct1.Id);
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

    [Test]
    public void SearchProducts_TestResponsiveDesign()
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

        MobileHomePage.SearchProduct("iPod Tou");
        MobileHomePage.SearchButton.Click();
        //SearchProductPage.OpenItem(expectedProduct1.Id);

        // version 1
        SearchProductPage.SearchInput.AssertAboveOf(SearchProductPage.SearchCategoriesSelect);
        SearchProductPage.SearchInput.AssertAboveOfGreaterThan(SearchProductPage.SearchCategoriesSelect, 2.0);
        SearchProductPage.SearchButton.AssertBelowOfLessThan(SearchProductPage.SearchCategoriesSelect, 20.0);

        SearchProductPage.ListViewButton.AssertRightOf(SearchProductPage.GridViewButton);
        SearchProductPage.GridViewButton.AssertLeftOf(SearchProductPage.ListViewButton);

        SearchProductPage.GridViewButton.AssertHeightGreaterThan(21);

        // version 2
        SearchProductPage.SearchInput.Above(SearchProductPage.SearchCategoriesSelect).Validate();
        SearchProductPage.SearchInput.Above(SearchProductPage.SearchCategoriesSelect).GreaterThan(2).Validate();
        SearchProductPage.SearchButton.Below(SearchProductPage.SearchCategoriesSelect).LessThan(20).Validate();

        SearchProductPage.ListViewButton.Right(SearchProductPage.GridViewButton).Validate();
        SearchProductPage.GridViewButton.Left(SearchProductPage.ListViewButton).Validate();

        SearchProductPage.GridViewButton.Height().GreaterThanOrEqual(21).Validate();
    }
}
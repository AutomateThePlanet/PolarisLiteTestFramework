﻿using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using DemoSystemTests.Integrations.Plugins.Blob;
using DemoSystemTests.Results;
using PolarisLite.Core;
using PolarisLite.Core.Layout.Second;
using PolarisLite.Web;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Web.Results;

[TestFixture]
[AllureNUnit]
//[LocalExecution(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
[LambdaTest]
[AllureEpic("E-commerce System Tests")]
[AllureFeature("Online Shopping Cart")]
[AllureParentSuite("EShop Tests")]
[AllureSeverity(SeverityLevel.critical)]
[AllureOwner("Anton Angelov")]
public class ProductPurchaseTests : ResultsWebTest
{
    public HomePage HomePage { get; private set; }
    public MobileHomePage MobileHomePage { get; private set; }
    public ProductPage ProductPage { get; private set; }
    public CartPage CartPage { get; private set; }
    public CheckoutPage CheckoutPage { get; private set; }
    public SearchProductPage SearchProductPage { get; private set; }

    protected override void Configure()
    {
        PluginExecutionEngine.AddPlugin(new TroubleshootingInfoAzurePublisherPlugin());
        base.Configure();
    }

    protected override void TestInitialize()
    {
        HomePage = App.Create<HomePage>();
        MobileHomePage = App.Create<MobileHomePage>();
        ProductPage = App.Create<ProductPage>();
        CartPage = App.Create<CartPage>();
        CheckoutPage = App.Create<CheckoutPage>();
        SearchProductPage = App.Create<SearchProductPage>();
        App.Navigation.GoToUrl("https://ecommerce-playground.lambdatest.io/");
    }

    [Test]
    [Category(Categories.CI)]
    [Category(Categories.WEB_SYSTEM_TESTS)]
    [AllureSuite("EShop Compare Products")]
    [AllureLink("LT Web", "https://www.lambdatest.com/support/api-doc/?key=selenium-automation-api/swagger.json")]
    [AllureIssue("UI-123")]
    [AllureTms("TMS-456")]
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

        ProductPage.AssertCompareProductDetails(expectedProduct1, 5);
        ProductPage.AssertCompareProductDetails(expectedProduct2, 2);
    }

    [Test]
    [Category(Categories.CI)]
    [Category(Categories.WEB_SYSTEM_TESTS)]
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

    [Test]
    [Category(Categories.WEB_SYSTEM_TESTS)]
    [LocalExecution(BrowserType.Chrome, Lifecycle.RestartEveryTime, mobileEmulation: true, deviceName: MobileDevices.GalaxyS20Ultra, MobileWindowSize._412_915, 1.0, userAgent: MobileUserAgents.GalaxyS20Ultra)]
    public void PurchaseTwoSameProduct_WhenSearchingWithoutAutocomplete_And_MobileEmulation()
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
    [LocalExecution(BrowserType.Chrome, Lifecycle.RestartEveryTime, mobileEmulation: true, deviceName: MobileDevices.GalaxyS20Ultra, MobileWindowSize._412_915, 1.0, userAgent: MobileUserAgents.GalaxyS20Ultra)]
    [Category(Categories.WEB_SYSTEM_TESTS)]
    public void SearchProducts_TestResponsiveDesign_And_MobileEmulation()
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

    [Test]
    [LambdaTest(BrowserType.Chrome, 120, DesktopWindowSize._1366_768)]
    [Category(Categories.WEB_SYSTEM_TESTS)]
    [Category(Categories.CI)]

    //[LocalExecution(Browser.Chrome, Lifecycle.RestartEveryTime, size: DesktopWindowSize._1366_768)]

    public void SearchProducts_TestResponsiveDesign_WhenResolutionIsSetTo_1366_768()
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

        HomePage.SearchProduct("iPod Tou");
        HomePage.SearchButton.Click();

        // version 1
        SearchProductPage.SearchInput.AssertLeftOf(SearchProductPage.SearchCategoriesSelect);
        SearchProductPage.SearchInput.AssertLeftOf(SearchProductPage.SearchCategoriesSelect, 10.0);
        SearchProductPage.SearchButton.AssertLeftOfLessThan(SearchProductPage.SearchCategoriesSelect, 20.0);

        SearchProductPage.ListViewButton.AssertRightOf(SearchProductPage.GridViewButton);
        SearchProductPage.GridViewButton.AssertLeftOf(SearchProductPage.ListViewButton);

        SearchProductPage.GridViewButton.AssertHeightGreaterThan(21);


        SearchProductPage.SearchInput.AssertBorderColor("rgb(206, 212, 218)");
        SearchProductPage.SearchInput.AssertFontFamily("\"Nunito Sans\", sans-serif");
        SearchProductPage.SearchInput.AssertFontSize("16px");
        SearchProductPage.SearchInput.AssertFontWeight("400");
        SearchProductPage.SearchInput.AssertTextAlign("start");
    }
}
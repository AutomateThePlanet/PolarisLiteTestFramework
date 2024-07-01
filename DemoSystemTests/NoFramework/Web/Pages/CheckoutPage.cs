using DemoSystemTests.Framework.Web.Pages.Models;

namespace DemoSystemTests.NoFramework;
public class CheckoutPage : WebPage
{
    public CheckoutPage(IWebDriver driver, WebDriverWait wait, Actions actions) 
        : base(driver, wait, actions)
    {
    }

    public IWebElement FirstNameInput => _driver.FindElement(By.Id("input-payment-firstname"));
    public IWebElement LastNameInput => _driver.FindElement(By.Id("input-payment-lastname"));
    public IWebElement EmailInput => _driver.FindElement(By.Id("input-payment-email"));
    public IWebElement TelephoneInput => _driver.FindElement(By.Id("input-payment-telephone"));
    public IWebElement PasswordInput => _driver.FindElement(By.Id("input-payment-password"));
    public IWebElement ConfirmPasswordInput => _driver.FindElement(By.Id("input-payment-confirm"));
    public IWebElement CompanyInput => _driver.FindElement(By.Id("input-payment-company"));
    public IWebElement Address1Input => _driver.FindElement(By.Id("input-payment-address-1"));
    public IWebElement Address2Input => _driver.FindElement(By.Id("input-payment-address-2"));
    public IWebElement CityInput => _driver.FindElement(By.Id("input-payment-city"));
    public IWebElement PostCodeInput => _driver.FindElement(By.Id("input-payment-postcode"));
    public IWebElement ShippingAddressCountrySelect => _driver.FindElement(By.Id("input-payment-country"));
    public IWebElement ShippingAddressCountryOption(string country) =>
        ShippingAddressCountrySelect.FindElement(By.XPath($".//option[contains(text(), '{country}')]"));
    public IWebElement BillingAddressRegionSelect => _driver.FindElement(By.Id("input-payment-zone"));
    public IWebElement BillingAddressRegionOption(string region) =>
        BillingAddressRegionSelect.FindElement(By.XPath($".//option[contains(text(), '{region}')]"));
    public IWebElement TermsAgreeCheckbox => _driver.FindElement(By.XPath("//input[@id='input-agree']//following-sibling::label"));
    public IWebElement ContinueButton => _driver.FindElement(By.XPath("//button[@id='button-save']"));
    public IWebElement TotalPrice => _driver.FindElements(By.XPath("//td[text()='Total:']/following-sibling::td/strong")).Last();

    public void FillUserDetails(UserDetails userDetails)
    {
        FirstNameInput.SendKeys(userDetails.FirstName);
        LastNameInput.SendKeys(userDetails.LastName);
        EmailInput.SendKeys(userDetails.Email);
        TelephoneInput.SendKeys(userDetails.Telephone);
        PasswordInput.SendKeys(userDetails.Password);
        ConfirmPasswordInput.SendKeys(userDetails.ConfirmPassword);
    }

    public void FillBillingAddress(BillingAddress billingAddress)
    {
        CompanyInput.SendKeys(billingAddress.Company);
        Address1Input.SendKeys(billingAddress.Address1);
        Address2Input.SendKeys(billingAddress.Address2);
        CityInput.SendKeys(billingAddress.City);
        PostCodeInput.SendKeys(billingAddress.PostCode);
        ShippingAddressCountrySelect.Click();
        ShippingAddressCountryOption(billingAddress.Country).Click();
        BillingAddressRegionSelect.Click();
        BillingAddressRegionOption(billingAddress.Region).Click();
    }

    public void AgreeToTerms()
    {
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", TermsAgreeCheckbox);
        TermsAgreeCheckbox.Click();
    }

    public void ClickContinue()
    {
        ContinueButton.Click();
    }

    public void AssertTotalPrice(string expectedPrice)
    {
        Assert.That(TotalPrice.Text, Is.EqualTo(expectedPrice));
    }

    public void CompleteCheckout()
    {
        var continueButton = _driver.FindElement(By.XPath("//button[@id='button-save']"));
        continueButton.Click();
    }
}
